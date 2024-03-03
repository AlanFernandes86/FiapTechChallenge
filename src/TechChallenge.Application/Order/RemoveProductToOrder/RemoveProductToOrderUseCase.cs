using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.RemoveProductToOrder
{
    public class RemoveProductToOrderUseCase : IUseCase<RemoveProductToOrderDAO, UseCaseOutput<int>>
    {
        private readonly IOrderRepository _orderRepository;

        public RemoveProductToOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<int>> Handle(RemoveProductToOrderDAO input)
        {
            try
            {
                var ordersByStatus = await _orderRepository.RemoveProductToOrder(input.OrderProductId);

                return new UseCaseOutput<int>(ordersByStatus);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<int>($"Erro ao remover produto do pedido - {ex.Message}");
            }
        }
    }
}
