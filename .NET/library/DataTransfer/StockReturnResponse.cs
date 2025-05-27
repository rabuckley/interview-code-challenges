namespace OneBeyondApi.DataTransfer;

public sealed record StockReturnResponse
{
    public Guid StockId { get; init; }

    public FineData? Fine { get; init; }
}
