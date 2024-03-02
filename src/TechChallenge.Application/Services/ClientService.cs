using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Ports.Services;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client?> GetClient(long cpf) => await _clientRepository.GetClient(cpf);

    public async Task<bool> PutClient(Client client) => await _clientRepository.PutClient(client);
}