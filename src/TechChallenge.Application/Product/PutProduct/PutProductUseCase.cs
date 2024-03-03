using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.PutProduct
{
    public class PutProductUseCase : IUseCase<PutProductDAO, UseCaseOutput<IEnumerable<Domain.Entities.Order>>>
    {
        private readonly IOrderRepository _orderRepository;

        public PutProductUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<IEnumerable<Domain.Entities.Order>>> Handle(PutProductDAO input)
        {
            var ordersByStatus = await _orderRepository.GetOrdersByStatus(input.OrderStatus);

            return new UseCaseOutput<IEnumerable<Domain.Entities.Order>>(ordersByStatus);
        }
    }
}
