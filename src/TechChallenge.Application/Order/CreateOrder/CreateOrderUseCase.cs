using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.CreateOrder
{
    public class CreateOrderUseCase : IUseCase<CreateOrderDAO, UseCaseOutput<IEnumerable<Domain.Entities.Order>>>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<IEnumerable<Domain.Entities.Order>>> Handle(CreateOrderDAO input)
        {
            var ordersByStatus = await _orderRepository.GetOrdersByStatus(input.OrderStatus);

            return new UseCaseOutput<IEnumerable<Domain.Entities.Order>>(ordersByStatus);
        }
    }
}
