using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.PutClient;

public class PutClientDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public PutClientDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
