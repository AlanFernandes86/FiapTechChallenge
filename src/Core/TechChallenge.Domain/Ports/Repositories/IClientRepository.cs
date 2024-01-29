using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Ports.Repositories;

public interface IClientRepository
{
    Task<bool> SetClient(Client client);
    Task<Client?> GetClient(long cpf);
}
