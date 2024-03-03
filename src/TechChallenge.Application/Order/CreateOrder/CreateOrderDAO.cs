using TechChallenge.Application.Common.UseCase.Interfaces;


namespace TechChallenge.Application.Order.CreateOrder;

public class CreateOrderDAO: IUseCaseDAO
{
    public long ClientCpf { get; set; } = 0;

    public List<OrderProductDAO> Products { get; set; }
}

public class OrderProductDAO: IUseCaseDAO
{
    public int ProductId { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
}