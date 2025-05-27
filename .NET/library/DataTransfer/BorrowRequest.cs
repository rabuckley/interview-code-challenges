using System;

namespace OneBeyondApi.DataTransfer;

/// <summary>
/// A request to borrow a stock item.
/// </summary>
public sealed record AddReservationRequest
{
    public Guid BorrowerId { get; init; }

    public Guid StockId { get; init; }
}
