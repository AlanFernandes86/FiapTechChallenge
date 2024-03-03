using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Schema;

namespace TechChallenge.Domain.Entities;

public class Order
{
    public int? Id { get; set; }

    public OrderStatus StatusId { get; set; }

    public long ClientCpf { get; set; }

    public List<OrderProduct> Products { get; set; }    

    public Order()
    {
        Products = new List<OrderProduct>();
    }

    public float Total => Products.Sum(x => x.Quantity * x.Price);
}
