using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.PutProductCategory;

public class PutProductCategoryDAO : IUseCaseDAO
{
    public readonly OrderStatus OrderStatus;

    public PutProductCategoryDAO(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
