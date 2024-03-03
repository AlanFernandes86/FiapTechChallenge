using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.PutProductToOrder
{
    public class PutProductToOrderUseCase : IUseCase<PutProductToOrderDAO, UseCaseOutput<int>>
    {
        private readonly IOrderRepository _orderRepository;

        public PutProductToOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<int>> Handle(PutProductToOrderDAO input)
        {
            try
            {
                var orderProduct = new Domain.Entities.ProductOnOrder
                {
                    ProductId = input.ProductId,
                    Price = input.Price,
                    Quantity = input.Quantity
                };

                var ordersByStatus = await _orderRepository.PutProductToOrder(orderProduct, input.OrderId);

                return new UseCaseOutput<int>(ordersByStatus);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<int>($"Erro ao inserir produto id: [{input.ProductId}] no pedido id: [{input.OrderId}] - {ex.Message}");
            }            
        }
    }
}
