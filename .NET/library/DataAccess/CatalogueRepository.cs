using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Controllers;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class CatalogueRepository : ICatalogueRepository
    {
        public CatalogueRepository()
        {
        }
        public List<BookStock> GetCatalogue()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .ToList();
                return list;
            }
        }

        public List<BookStock> SearchCatalogue(CatalogueSearch search)
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .AsQueryable();

                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.Author))
                    {
                        list = list.Where(x => x.Book.Author.Name.Contains(search.Author));
                    }
                    if (!string.IsNullOrEmpty(search.BookName))
                    {
                        list = list.Where(x => x.Book.Name.Contains(search.BookName));
                    }
                }

                return list.ToList();
            }
        }

        public List<LoanDetail> GetLoanDetails()
        {
            using var context = new LibraryContext();

            var loanedStock = context.Catalogue
                .AsNoTracking()
                .Include(x => x.OnLoanTo)
                .Include(x => x.Book)
                .Where(x => x.OnLoanTo != null)
                .GroupBy(x => x.OnLoanTo)
                .Select(g => new LoanDetail
                {
                    Borrower = g.Key!,
                    LoanedBookTitles = g.Select(x => x.Book.Name).ToList()
                });

            return [.. loanedStock];
        }

        public BookStockReturnResult ReturnStock(Guid stockId)
        {
            using var context = new LibraryContext();

            var stock = context.Catalogue
                .Include(x => x.OnLoanTo)
                .FirstOrDefault(x => x.Id == stockId);

            if (stock is null)
            {
                return BookStockReturnResult.NotFound(stockId);
            }

            if (stock.OnLoanTo is null)
            {
                return BookStockReturnResult.NotOnLoan(stockId);
            }

            stock.OnLoanTo = null;
            stock.LoanEndDate = null;

            context.SaveChanges();
            return BookStockReturnResult.Returned(stock);
        }
    }
}

