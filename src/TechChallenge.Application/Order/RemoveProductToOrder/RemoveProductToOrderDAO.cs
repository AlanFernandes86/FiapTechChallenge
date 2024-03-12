using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.RemoveProductToOrder;

public class RemoveProductToOrderDAO: IUseCaseDAO
{
    public readonly int OrderProductId;

    public RemoveProductToOrderDAO(int orderProductId)
    {
        OrderProductId = orderProductId;
    }
}
