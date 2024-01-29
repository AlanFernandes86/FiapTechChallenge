using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Ports.Repositories;
using TechChallenge.Domain.Ports.Services;

namespace TechChallenge.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public Task<IEnumerable<ProductCategory>?> GetProductCategories() => _productRepository.GetProductCategories();

    public Task<IEnumerable<Product>?> GetProductsByCategory(int categoryId) => _productRepository.GetProductsByCategory(categoryId);

    public Task<int> SetProduct(Product product) => _productRepository.SetProduct(product);

    public Task<bool> SetProductCategory(ProductCategory productCategory) => _productRepository.SetProductCategory(productCategory);
}
