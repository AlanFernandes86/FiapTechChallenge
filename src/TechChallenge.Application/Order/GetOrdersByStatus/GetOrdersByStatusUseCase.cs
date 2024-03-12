using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.GetOrdersByStatus
{
    public class GetOrdersByStatusUseCase : IUseCase<GetOrdersByStatusDAO, UseCaseOutput<IEnumerable<Domain.Entities.Order>>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByStatusUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<IEnumerable<Domain.Entities.Order>>> Handle(GetOrdersByStatusDAO input)
        {
            try
            {
                var ordersByStatus = await _orderRepository.GetOrdersByStatus(input.OrderStatus);

                return new UseCaseOutput<IEnumerable<Domain.Entities.Order>>(ordersByStatus);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<IEnumerable<Domain.Entities.Order>>($"Erro ao consultar pedidos pelo status: [{input.OrderStatus}] - {ex.Message}");
            }            
        }
    }
}
