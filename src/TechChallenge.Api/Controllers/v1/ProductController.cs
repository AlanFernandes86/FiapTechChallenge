using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Common.UseCase.Extensions;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Application.Order.GetProductCategories;
using TechChallenge.Application.Order.GetProductsByCategory;
using TechChallenge.Application.Order.PutProduct;
using TechChallenge.Application.Order.PutProductCategory;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IUseCase<GetProductCategoriesDAO, UseCaseOutput<IEnumerable<ProductCategory>>> _getProductCategoriesUseCase;
    private readonly IUseCase<GetProductsByCategoryDAO, UseCaseOutput<IEnumerable<Product>>> _getProductsByCategoryUseCase;
    private readonly IUseCase<PutProductDAO, UseCaseOutput<int>> _putProductUseCase;
    private readonly IUseCase<PutProductCategoryDAO, UseCaseOutput<int>> _putProductCategoryUseCase;

    public ProductController(
        IUseCase<GetProductCategoriesDAO, UseCaseOutput<IEnumerable<ProductCategory>>> getProductCategoriesUseCase,
        IUseCase<GetProductsByCategoryDAO, UseCaseOutput<IEnumerable<Product>>> getProductsByCategoryUseCase,
        IUseCase<PutProductDAO, UseCaseOutput<int>> putProductUseCase,
        IUseCase<PutProductCategoryDAO, UseCaseOutput<int>> putProductCategoryUseCase
        )
    {
        _getProductCategoriesUseCase = getProductCategoriesUseCase;
        _getProductsByCategoryUseCase = getProductsByCategoryUseCase;
        _putProductUseCase = putProductUseCase;
        _putProductCategoryUseCase = putProductCategoryUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var output = await _getProductsByCategoryUseCase.Handle(new GetProductsByCategoryDAO(categoryId));

        return output.ToResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> PutProduct(PutProductDAO putProductDAO)
    {
        var output = await _putProductUseCase.Handle(putProductDAO);

        return output.ToResult(this);
    }

    [HttpGet("Categories")]
    public async Task<IActionResult> GetProductCategories()
    {
        var output = await _getProductCategoriesUseCase.Handle(new GetProductCategoriesDAO());

        return output.ToResult(this);
    }

    [HttpPut("Category")]
    public async Task<IActionResult> PutProductCategory(PutProductCategoryDAO putProductCategoryDAO)
    {
        var output = await _putProductCategoryUseCase.Handle(putProductCategoryDAO);

        return output.ToResult(this);
    }
}