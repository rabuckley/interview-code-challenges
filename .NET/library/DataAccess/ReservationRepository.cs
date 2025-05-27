using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess;

public sealed class ReservationRepository : IReservationRepository
{
    public AddReservationResult AddReservation(Guid borrowerId, Guid stockId)
    {
        using var context = new LibraryContext();

        var borrower = context.Borrowers.Find(borrowerId);

        if (borrower is null)
        {
            return AddReservationResult.BorrowerNotFound(borrowerId);
        }

        var stock = context.Catalogue
            .Include(x => x.OnLoanTo)
            .Include(x => x.Reservations)
            .FirstOrDefault(x => x.Id == stockId);

        if (stock is null)
        {
            return AddReservationResult.NotFound(stockId);
        }

        if (stock.OnLoanTo is null)
        {
            return AddReservationResult.NotOnLoan(stockId);
        }

        var lastReservation = stock.Reservations
            .OrderByDescending(r => r.ReservedFrom)
            .FirstOrDefault();

        var reservationStart = lastReservation?.ReservedTo ?? DateTime.UtcNow;

        var reservation = new ReservationData
        {
            Borrower = borrower,
            ReservedFrom = reservationStart,
            ReservedTo = reservationStart.AddDays(7)
        };

        stock.Reservations.Add(reservation);

        context.SaveChanges();

        return AddReservationResult.Success(reservation);
    }

    public List<ReservationData> SearchReservations(ReservationSearch search)
    {
        using var context = new LibraryContext();

        var query = context.Reservations.AsNoTracking().AsQueryable();

        if (search is null)
        {
            return query.ToList();
        }

        if (search.ReservationId.HasValue)
        {
            query = query.Where(r => r.Id == search.ReservationId.Value);
        }

        if (search.BorrowerId.HasValue)
        {
            query = query.Where(r => r.BorrowerId == search.BorrowerId.Value);
        }

        if (search.StockId.HasValue)
        {
            query = query.Where(r => r.StockId == search.StockId.Value);
        }

        return query.ToList();
    }
}
