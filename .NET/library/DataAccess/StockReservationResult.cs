using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess;

public abstract class AddReservationResult
{
    public static ReservationStockNotFoundResult NotFound(Guid stockId) => new() { StockId = stockId };

    public static ReservationStockNotOnLoanResult NotOnLoan(Guid stockId) => new() { StockId = stockId };

    public static ReservationSuccessResult Success(ReservationData reservation) => new() { Reservation = reservation };

    public static AddReservationResult BorrowerNotFound(Guid borrowerId) => new ReservationBorrowerNotFoundResult { BorrowerId = borrowerId };
}

public sealed class ReservationStockNotFoundResult : AddReservationResult
{
    public required Guid StockId { get; init; }
}

public sealed class ReservationStockNotOnLoanResult : AddReservationResult
{
    public required Guid StockId { get; init; }
}

public sealed class ReservationSuccessResult : AddReservationResult
{
    public required ReservationData Reservation { get; init; }
}


public sealed class ReservationBorrowerNotFoundResult : AddReservationResult
{
    public required Guid BorrowerId { get; init; }
}
