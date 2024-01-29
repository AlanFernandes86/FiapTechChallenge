using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Ports.Services;

namespace TechChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> SetProduct(Product product)
        {
            var result = await _productService.SetProduct(product);

            return result != -1 ? Ok(new { id = result }) : BadRequest();
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetProductCategory()
        {
            var result = await _productService.GetProductCategories();

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPut("Category")]
        public async Task<IActionResult> SetProductCategory(ProductCategory productCategory)
        {
            var result = await _productService.SetProductCategory(productCategory);

            return result ? Ok() : BadRequest();
        }        
    }
}