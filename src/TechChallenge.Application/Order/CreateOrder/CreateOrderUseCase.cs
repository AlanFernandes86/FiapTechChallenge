using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.CreateOrder
{
    public class CreateOrderUseCase : IUseCase<CreateOrderDAO, UseCaseOutput<int>>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<UseCaseOutput<int>> Handle(CreateOrderDAO input)
        {
            try
            {
                var order = new Domain.Entities.Order
                {
                    StatusId = OrderStatus.CREATED,
                    ClientCpf = input.ClientCpf,
                    ProductsOnOrder = input.Products.Select(x => new ProductOnOrder
                    {
                        ProductId = x.ProductId,
                        Price = x.Price,
                        Quantity = x.Quantity
                    }).ToList()
                };

                var ordersByStatus = await _orderRepository.CreateOrder(order);

                return new UseCaseOutput<int>(ordersByStatus);
            }
            catch (Exception ex)
            {
                var cpfOrAnonymous = input.ClientCpf == 0 ? "Anônimo" : input.ClientCpf.ToString();
                return new UseCaseOutput<int>($"Erro ao criar novo pedido para o cliente cpf: [{cpfOrAnonymous}] - {ex.Message}");
            }
            
        }
    }
}
