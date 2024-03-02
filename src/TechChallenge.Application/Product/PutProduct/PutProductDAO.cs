using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.PutProduct;

public class PutProductDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public PutProductDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
