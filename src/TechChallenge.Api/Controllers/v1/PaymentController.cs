using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Application.Order.SetPayment;

namespace TechChallenge.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IUseCase<SetPaymentDAO, UseCaseOutput<int>> _setPaymentUseCase;

    public PaymentController(IUseCase<SetPaymentDAO, UseCaseOutput<int>> setPaymentUseCase)
    {
        _setPaymentUseCase = setPaymentUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UseCaseOutput<int>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(UseCaseOutput<int>))]
    public async Task<IActionResult> SetPayment(SetPaymentDAO setPaymentDAO, CancellationToken cancellationToken)
    {
        var result = await _setPaymentUseCase.Handle(setPaymentDAO, cancellationToken);

        if (result.OutputStatus == OutputStatus.Success)
            return Ok(result);

        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}