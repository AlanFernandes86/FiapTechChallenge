using TechChallenge.Application.Common.UseCase.Interfaces;

namespace TechChallenge.Application.Order.PutProductToOrder;

public class PutProductToOrderDAO: IUseCaseDAO
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
}
