using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.DataTransfer;

namespace OneBeyondApi.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ReservationController : ControllerBase
{
    private readonly IReservationRepository _repository;

    public ReservationController(IReservationRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    [Route("AddReservation")]
    [ProducesResponseType(typeof(DataTransfer.ReservationData), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Reserve([FromBody] AddReservationRequest request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        if (request.BorrowerId == Guid.Empty || request.StockId == Guid.Empty)
        {
            return BadRequest("BorrowerId and StockId must be provided.");
        }

        var result = _repository.AddReservation(request.BorrowerId, request.StockId);

        return result switch
        {
            ReservationStockNotFoundResult stockNotFound => BadRequest($"Stock with ID '{stockNotFound.StockId}' not found."),
            ReservationBorrowerNotFoundResult borrowerNotFound => BadRequest($"Borrower with ID '{borrowerNotFound.BorrowerId}' not found."),
            ReservationStockNotOnLoanResult notOnLoan => BadRequest($"Stock with ID '{notOnLoan.StockId}' is not currently on loan."),
            ReservationSuccessResult success => Ok(new DataTransfer.ReservationData
            {
                Id = success.Reservation.Id,
                StockId = success.Reservation.Stock.Id,
                BorrowerId = success.Reservation.Borrower.Id,
                ReservedFrom = success.Reservation.ReservedFrom,
                ReservedTo = success.Reservation.ReservedTo
            }),
            _ => throw new UnreachableException($"Unexpected result type `{result.GetType()}`")
        };
    }

    [HttpPost]
    [Route("SearchReservations")]
    [ProducesResponseType(typeof(IList<DataTransfer.ReservationData>), StatusCodes.Status200OK)]
    public IActionResult SearchReservations([FromBody] ReservationSearch search)
    {
        if (search is null)
        {
            return BadRequest();
        }

        var reservations = _repository.SearchReservations(search).Select(r => new DataTransfer.ReservationData
        {
            Id = r.Id,
            StockId = r.StockId,
            BorrowerId = r.BorrowerId,
            ReservedFrom = r.ReservedFrom,
            ReservedTo = r.ReservedTo
        }).ToList();

        return Ok(reservations);
    }
}
