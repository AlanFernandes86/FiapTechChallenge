using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Ports.Services;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Domain.Entities.Order>> GetOrderByStatus(OrderStatus orderStatus) => await _orderRepository.GetOrdersByStatus(orderStatus);

    public async Task<int> CreateOrder(Domain.Entities.Order order) => await _orderRepository.CreateOrder(order);

    public async Task<int> PutProductToOrder(OrderProduct orderProduct, int orderId) => await _orderRepository.PutProductToOrder(orderProduct, orderId);

    public async Task<Domain.Entities.Order?> GetOrdersById(int orderId) => await _orderRepository.GetOrdersById(orderId);

    public async Task<int> RemoveProductToOrder(int orderProductId) => await _orderRepository.RemoveProductToOrder(orderProductId);

    public async Task<int> UpdateOrderStatus(int orderId, OrderStatus orderStatus) => await _orderRepository.UpdateOrderStatus(orderId, orderStatus);
}
