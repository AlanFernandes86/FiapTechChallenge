using TechChallenge.Application.Common.UseCase.Interfaces;


namespace TechChallenge.Application.Order.CreateOrder;

public class CreateOrderDAO: IUseCaseDAO
{
    public string ClientCpf { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public List<ProductOnOrderDAO> Products { get; set; }
}

public class ProductOnOrderDAO: IUseCaseDAO
{
    public int ProductId { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
}