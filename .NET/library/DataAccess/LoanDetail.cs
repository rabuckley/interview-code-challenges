using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess;

/// <summary>
/// Represents a summary of the loaned books for a given borrower.
/// </summary>
public sealed record LoanDetail
{
    public required Borrower Borrower { get; init; }

    public required List<string> LoanedBookTitles { get; init; }
}