using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.UpdateOrderStatus
{
    public class UpdateOrderStatusUseCase : IUseCase<UpdateOrderStatusDAO, UseCaseOutput<int>>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderStatusUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<int>> Handle(UpdateOrderStatusDAO input)
        {
            try
            {
                var orderId = await _orderRepository.UpdateOrderStatus(input.OrderId, input.OrderStatus);

                if (orderId == -1)
                {
                    return new UseCaseOutput<int>(new Validation("ORDER_NOT_FOUND", $"pedido id: [{input.OrderId}] - não encontrado"));
                }

                return new UseCaseOutput<int>(orderId);
            }
            catch (Exception)
            {
                return new UseCaseOutput<int>($"Erro ao atualizar status do pedido id: [{input.OrderId}]");
            }            
        }
    }
}
