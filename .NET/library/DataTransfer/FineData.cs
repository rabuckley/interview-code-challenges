namespace OneBeyondApi.DataTransfer;

public sealed record FineData
{
    public required Guid FineId { get; init; }

    public required Guid BorrowerId { get; init; }

    public required decimal Amount { get; init; }

    public required DateTimeOffset DateIssued { get; init; }

    public required DateTimeOffset? DatePaid { get; init; }
}