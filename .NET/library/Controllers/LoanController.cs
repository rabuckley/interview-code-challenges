using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

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
    public IActionResult Return(Guid stockId)
    {
        var result = _catalogueRepository.ReturnStock(stockId);

        return result switch
        {
            BookStockReturnSuccess s => Ok(s.Stock),
            BookStockNotFound s => NotFound(s.StockId),
            BookStockNotOnLoan s => BadRequest($"Stock with ID '{s.StockId}' is not currently on loan."),
            _ => throw new UnreachableException($"Unexpected result type `{result.GetType()}`")
        };
    }
}
