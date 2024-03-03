using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.RemoveProductToOrder
{
    public class RemoveProductToOrderUseCase : IUseCase<RemoveProductToOrderDAO, UseCaseOutput<IEnumerable<Domain.Entities.Order>>>
    {
        private readonly IOrderRepository _orderRepository;

        public RemoveProductToOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<IEnumerable<Domain.Entities.Order>>> Handle(RemoveProductToOrderDAO input)
        {
            var ordersByStatus = await _orderRepository.GetOrdersByStatus(input.OrderStatus);

            return new UseCaseOutput<IEnumerable<Domain.Entities.Order>>(ordersByStatus);
        }
    }
}
