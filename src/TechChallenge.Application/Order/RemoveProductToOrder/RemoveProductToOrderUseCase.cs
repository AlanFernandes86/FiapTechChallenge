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
                var orderProductId = await _orderRepository.RemoveProductToOrder(input.OrderProductId);

                if (orderProductId == -1)
                {
                    return new UseCaseOutput<int>(new Validation("PRODUCT_ON_ORDER_NOT_FOUND", $"produto no pedido id: [{input.OrderProductId}] - não encontrado ou já removido"));
                }

                return new UseCaseOutput<int>(orderProductId);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<int>($"Erro ao remover produto do pedido - {ex.Message}");
            }
        }
    }
}
