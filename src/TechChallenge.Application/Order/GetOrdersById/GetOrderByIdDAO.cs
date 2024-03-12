using TechChallenge.Application.Common.UseCase.Interfaces;

namespace TechChallenge.Application.Order.GetOrdersById;

public class GetOrderByIdDAO: IUseCaseDAO
{
    public readonly int OrderId;

    public GetOrderByIdDAO(int orderId)
    {
        OrderId = orderId;
    }
}
