using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Ports.Repositories;

public interface IProductRepository
{
    Task<bool> SetProductCategory(ProductCategory productCategory);
    Task<int> SetProduct(Product product);
    Task<IEnumerable<ProductCategory>?> GetProductCategories();
    Task<IEnumerable<Product>?> GetProductsByCategory(int categoryId);
}
