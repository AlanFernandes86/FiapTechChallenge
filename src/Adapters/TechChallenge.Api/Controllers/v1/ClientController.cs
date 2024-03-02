using Microsoft.AspNetCore.Mvc;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Ports.Services;

namespace TechChallenge.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetClient(long cpf)
    {
        var result = await _clientService.GetClient(cpf);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> PutClient(Client client)
    {
        var result = await _clientService.PutClient(client);

        return result ? Ok() : BadRequest();
    }
}