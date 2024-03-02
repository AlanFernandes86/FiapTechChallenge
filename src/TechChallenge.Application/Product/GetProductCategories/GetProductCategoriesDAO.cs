using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.GetProductCategories;

public class GetProductCategoriesDAO: IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public GetProductCategoriesDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
