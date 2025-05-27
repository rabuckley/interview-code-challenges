using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess;

public interface IReservationRepository
{
    public AddReservationResult AddReservation(Guid borrowerId, Guid stockId);

    public List<ReservationData> SearchReservations(ReservationSearch search);
}