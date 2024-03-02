using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.GetProductsByCategory;

public class GetProductsByCategoryDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public GetProductsByCategoryDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
