using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Ports.Services;

public interface IClientService
{
    Task<bool> SetClient(Client client);
    Task<Client?> GetClient(long cpf);
}