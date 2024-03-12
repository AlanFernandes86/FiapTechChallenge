using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.GetOrdersById
{
    public class GetOrderByIdUseCase : IUseCase<GetOrderByIdDAO, UseCaseOutput<Domain.Entities.Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<Domain.Entities.Order>> Handle(GetOrderByIdDAO input)
        {
            try
            {
                var order = await _orderRepository.GetOrdersById(input.OrderId);

                if (order == null)
                {
                    return new UseCaseOutput<Domain.Entities.Order>(new Validation("ORDER_NOT_FOUND", $"pedido id: [{input.OrderId}] - não encontrado"));
                }

                return new UseCaseOutput<Domain.Entities.Order>(order);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<Domain.Entities.Order>($"Erro ao buscar pedido pelo id: [{input.OrderId}] - {ex.Message}");
            }            
        }
    }
}
