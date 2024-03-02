using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Ports.Repositories;

public interface IClientRepository
{
    Task<bool> PutClient(Client client);
    Task<Client?> GetClient(long cpf);
}
