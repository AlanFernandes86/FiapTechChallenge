using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.GetClient
{
    public class GetClientUseCase : IUseCase<GetClientDAO, UseCaseOutput<Domain.Entities.Client>>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientUseCase(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<UseCaseOutput<Domain.Entities.Client>> Handle(GetClientDAO input)
        {
            try
            {
                var ordersByStatus = await _clientRepository.GetClient(input.Cpf);

                return new UseCaseOutput<Domain.Entities.Client>(ordersByStatus);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<Domain.Entities.Client>($"Erro ao consultar cliente cpf: [{input.Cpf}] - {ex.Message}");
            }
            
        }
    }
}
