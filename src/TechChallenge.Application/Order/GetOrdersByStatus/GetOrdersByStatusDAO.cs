using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.GetOrdersByStatus;

public class GetOrdersByStatusDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public GetOrdersByStatusDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
