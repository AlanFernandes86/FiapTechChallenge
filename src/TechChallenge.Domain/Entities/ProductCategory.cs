namespace TechChallenge.Domain.Entities;

public class ProductCategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ProductCategory(string name, int id)
    {
        Id = id;
        Name = name;
    }

    public ProductCategory()
    {
        Name = string.Empty;
    }
}
