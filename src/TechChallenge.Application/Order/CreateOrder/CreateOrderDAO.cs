using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.CreateOrder;

public class CreateOrderDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public CreateOrderDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
