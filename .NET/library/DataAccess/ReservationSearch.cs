namespace OneBeyondApi.DataAccess
{
    public sealed record ReservationSearch
    {
        public Guid? ReservationId { get; init; }

        public Guid? StockId { get; init; }

        public Guid? BorrowerId { get; init; }
    }
}