﻿using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Ports.Services;

public interface IProductService
{
    Task<bool> PutProductCategory(ProductCategory productCategory);
    Task<int> PutProduct(Product product);
    Task<IEnumerable<ProductCategory>?> GetProductCategories();
    Task<IEnumerable<Product>?> GetProductsByCategory(int categoryId);
}