using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Repositories;

public interface IClientRepository
{
    Task<bool> PutClient(Client client);
    Task<Client> GetClient(long cpf);
}
