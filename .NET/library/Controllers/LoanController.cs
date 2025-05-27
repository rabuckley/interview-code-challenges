using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess;

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
}
