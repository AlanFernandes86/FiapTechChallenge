using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.UpdateOrderStatus;

public class UpdateOrderStatusDAO: IUseCaseDAO
{
    public int OrderId;
    public OrderStatus OrderStatus;

    public UpdateOrderStatusDAO(int orderId, OrderStatus orderStatus)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
    }
}
