using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.PutClient
{
    public class PutClientUseCase : IUseCase<PutClientDAO, UseCaseOutput<bool>>
    {
        private readonly IClientRepository _clientRepository;

        public PutClientUseCase(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<UseCaseOutput<bool>> Handle(PutClientDAO input, CancellationToken cancellationToken)
        {
            try
            {
                var client = new Domain.Entities.Client(input.Cpf, input.Name, input.Email);

                var ordersByStatus = await _clientRepository.PutClient(client);

                return new UseCaseOutput<bool>(ordersByStatus);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<bool>($"Erro ao cadastrar ou atualizar cliente na base de dados - {ex.Message}");
            }
            
        }
    }
}
