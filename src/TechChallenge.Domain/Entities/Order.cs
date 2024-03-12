using TechChallenge.Domain.Enums;

namespace TechChallenge.Domain.Entities;

public class Order
{
    public int? Id { get; set; }

    public OrderStatus StatusId { get; set; }

    public string ClientCpf { get; set; }
    public string ClientName { get; set; }

    public List<ProductOnOrder> ProductsOnOrder { get; set; }    

    public Order()
    {
        ProductsOnOrder = new List<ProductOnOrder>();
    }

    public float Total => ProductsOnOrder.Sum(x => x.Quantity * x.Price);
}
