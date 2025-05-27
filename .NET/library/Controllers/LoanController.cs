using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.DataTransfer;
using OneBeyondApi.Mapping;

namespace OneBeyondApi.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class LoanController : ControllerBase
{
    private readonly ICatalogueRepository _catalogueRepository;

    public LoanController(ICatalogueRepository catalogueRepository)
    {
        _catalogueRepository = catalogueRepository;
    }

    [HttpGet]
    [Route("GetLoans")]
    public IList<LoanDetail> Get()
    {
        return _catalogueRepository.GetLoanDetails();
    }

    [HttpPost]
    [Route("Return")]
    [ProducesResponseType(typeof(StockReturnResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Return(Guid stockId)
    {
        var result = _catalogueRepository.ReturnStock(stockId);

        return result switch
        {
            BookStockReturnSuccess s => Ok(new StockReturnResponse
            {
                StockId = s.Stock.Id,
                Fine = FineModelToDataMapper.CreateMapped(s.Fine)
            }),
            BookStockNotFound s => NotFound(s.StockId),
            BookStockNotOnLoan s => BadRequest($"Stock with ID '{s.StockId}' is not currently on loan."),
            _ => throw new UnreachableException($"Unexpected result type `{result.GetType()}`")
        };
    }
}
