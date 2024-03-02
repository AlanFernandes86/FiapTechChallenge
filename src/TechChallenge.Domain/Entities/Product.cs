namespace TechChallenge.Domain.Entities;

public class Product
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public int CategoryId { get; set; }

    public Product(string name, string description, float price, int categoryId, int? id = null)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }

    public Product()
    {
        Id = null;
        Name = string.Empty;
        Description = string.Empty;
    }
}
