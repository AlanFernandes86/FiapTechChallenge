using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Domain.Repositories;

public interface IOrderRepository
{
    Task<int> CreateOrder(Order order);
    Task<int> PutProductToOrder(ProductOnOrder orderProduct, int orderId);
    Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus orderStatus);
    Task<Order?> GetOrdersById(int orderId);
    Task<int> RemoveProductToOrder(int orderProductId);
    Task<int> UpdateOrderStatus(int orderId, OrderStatus orderStatus);
}
