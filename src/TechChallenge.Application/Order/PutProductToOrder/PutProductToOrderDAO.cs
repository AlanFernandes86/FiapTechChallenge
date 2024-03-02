using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.PutProductToOrder;

public class PutProductToOrderDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public PutProductToOrderDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
