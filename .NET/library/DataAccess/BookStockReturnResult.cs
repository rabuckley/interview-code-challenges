using OneBeyondApi.Model;

public abstract class BookStockReturnResult
{
    public static BookStockReturnResult NotFound(Guid stockId) => new BookStockNotFound(stockId);

    public static BookStockReturnResult NotOnLoan(Guid stockId) => new BookStockNotOnLoan(stockId);

    public static BookStockReturnResult Returned(BookStock stock, Fine? fine) => new BookStockReturnSuccess { Stock = stock, Fine = fine };
}

public sealed class BookStockNotFound : BookStockReturnResult
{
    public Guid StockId { get; }

    public BookStockNotFound(Guid stockId)
    {
        StockId = stockId;
    }
}

public sealed class BookStockNotOnLoan : BookStockReturnResult
{
    public Guid StockId { get; }

    public BookStockNotOnLoan(Guid stockId)
    {
        StockId = stockId;
    }
}

public sealed class BookStockReturnSuccess : BookStockReturnResult
{
    public required BookStock Stock { get; init; }

    public required Fine? Fine { get; init; }
}
