namespace OneBeyondApi.Model;

/// <summary>
/// Models a reservation of a stock item.
/// </summary>
public sealed class ReservationData
{
    public Guid Id { get; set; }

    public Guid StockId { get; internal set; }
    public BookStock Stock { get; set; }

    public Guid BorrowerId { get; set; }
    public Borrower Borrower { get; set; }

    public DateTime ReservedFrom { get; set; }

    public DateTime ReservedTo { get; set; }
}