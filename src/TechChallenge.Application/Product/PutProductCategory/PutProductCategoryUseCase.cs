using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.PutProductCategory
{
    public class PutProductCategoryUseCase : IUseCase<PutProductCategoryDAO, UseCaseOutput<IEnumerable<Domain.Entities.Order>>>
    {
        private readonly IOrderRepository _orderRepository;

        public PutProductCategoryUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<IEnumerable<Domain.Entities.Order>>> Handle(PutProductCategoryDAO input)
        {
            var ordersByStatus = await _orderRepository.GetOrdersByStatus(input.OrderStatus);

            return new UseCaseOutput<IEnumerable<Domain.Entities.Order>>(ordersByStatus);
        }
    }
}
