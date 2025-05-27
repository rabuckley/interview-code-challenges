namespace OneBeyondApi.DataTransfer;

public sealed record ReservationData
{
    public required Guid Id { get; init; }

    public required Guid StockId { get; init; }

    public required Guid BorrowerId { get; init; }

    public required DateTime ReservedFrom { get; init; }

    public required DateTime ReservedTo { get; init; }
}
