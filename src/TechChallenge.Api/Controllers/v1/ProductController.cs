using Microsoft.AspNetCore.Mvc;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Ports.Services;

namespace TechChallenge.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var result = await _productService.GetProductsByCategory(categoryId);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> PutProduct(Product product)
    {
        var result = await _productService.PutProduct(product);

        return result != -1 ? Ok(new { id = result }) : BadRequest();
    }

    [HttpGet("Categories")]
    public async Task<IActionResult> GetProductCategory()
    {
        var result = await _productService.GetProductCategories();

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPut("Category")]
    public async Task<IActionResult> PutProductCategory(ProductCategory productCategory)
    {
        var result = await _productService.PutProductCategory(productCategory);

        return result ? Ok() : BadRequest();
    }
}