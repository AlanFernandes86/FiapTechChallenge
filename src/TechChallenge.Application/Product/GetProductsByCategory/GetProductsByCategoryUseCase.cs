using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.GetProductsByCategory
{
    public class GetProductsByCategoryUseCase : IUseCase<GetProductsByCategoryDAO, UseCaseOutput<IEnumerable<Product>>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByCategoryUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<UseCaseOutput<IEnumerable<Product>>> Handle(GetProductsByCategoryDAO input)
        {
            try
            {
                var products = await _productRepository.GetProductsByCategory(input.CategoryId);

                return new UseCaseOutput<IEnumerable<Product>>(products);
            }
            catch (Exception)
            {
                return new UseCaseOutput<IEnumerable<Product>>($"Erro ao buscar produtos pela categoria id: [{input.CategoryId}]");
            }
            
        }
    }
}
