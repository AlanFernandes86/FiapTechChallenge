using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Ports.Services;
using TechChallenge.Domain.Repositories;

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

    public Task<int> PutProduct(Product product) => _productRepository.PutProduct(product);

    public Task<int> PutProductCategory(ProductCategory productCategory) => _productRepository.PutProductCategory(productCategory);
}
