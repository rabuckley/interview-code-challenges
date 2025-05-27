namespace OneBeyondApi.Model;

public sealed class Fine
{
    public Guid Id { get; set; }

    public Guid BorrowerId { get; set; }
    public Borrower Borrower { get; set; }

    /// <summary>
    /// The amount of the fine in GBP.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// The date the fine was issued.
    /// </summary>
    public DateTimeOffset DateIssued { get; set; }

    /// <summary>
    /// The date the fine was paid, if paid.
    /// </summary>
    public DateTimeOffset? DatePaid { get; set; }
}
