using Microsoft.AspNetCore.Mvc;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Ports.Services;

namespace TechChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> SetClient(Client client)
        {
            var result = await _clientService.SetClient(client);

            return result ? Ok() : BadRequest();
        }
    }
}