using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Application.Order.GetClient;
using TechChallenge.Application.Order.PutClient;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IUseCase<GetClientDAO, UseCaseOutput<Client>> _getClientUseCase;
    private readonly IUseCase<PutClientDAO, UseCaseOutput<bool>> _putClientUseCase;

    public ClientController(
        IUseCase<GetClientDAO, UseCaseOutput<Client>> getClientUseCase,
        IUseCase<PutClientDAO, UseCaseOutput<bool>> putClientUseCase
    )
    {
        _getClientUseCase = getClientUseCase;
        _putClientUseCase = putClientUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetClient(long cpf)
    {
        var result = await _getClientUseCase.Handle(new GetClientDAO(cpf));

        if (result.OutputStatus == OutputStatus.Success)
            return Ok(result);

        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPut]
    public async Task<IActionResult> PutClient(PutClientDAO putClientDAO)
    {
        var result = await _putClientUseCase.Handle(putClientDAO);

        if (result.OutputStatus == OutputStatus.Success)
            return Ok(result);

        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}