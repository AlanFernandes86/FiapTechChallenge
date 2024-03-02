using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.GetOrdersById;

public class GetOrdersByIdDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public GetOrdersByIdDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
