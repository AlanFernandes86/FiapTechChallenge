using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Domain.Ports.Services;

public interface IOrderService
{
    Task<int> CreateOrder(Order order);
    Task<int> PutProductToOrder(OrderProduct orderProduct, int orderId);
    Task<IEnumerable<Order>> GetOrderByStatus(OrderStatus orderStatus);
    Task<Order?> GetOrdersById(int orderId);
    Task<int> RemoveProductToOrder(int orderProductId);
}
