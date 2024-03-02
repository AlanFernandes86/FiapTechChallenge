using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.UpdateOrderStatus;

public class UpdateOrderStatusDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public UpdateOrderStatusDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
