using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Repositories;

public interface IProductRepository
{
    Task<bool> PutProductCategory(ProductCategory productCategory);
    Task<int> PutProduct(Product product);
    Task<IEnumerable<ProductCategory>?> GetProductCategories();
    Task<IEnumerable<Product>?> GetProductsByCategory(int categoryId);
}
