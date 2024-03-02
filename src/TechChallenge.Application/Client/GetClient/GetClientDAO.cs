using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.GetClient;

public class GetClientDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public GetClientDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
