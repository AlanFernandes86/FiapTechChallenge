using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.RemoveProductToOrder;

public class RemoveProductToOrderDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public RemoveProductToOrderDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
