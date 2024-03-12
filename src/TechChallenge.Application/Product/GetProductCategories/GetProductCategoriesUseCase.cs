using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.GetProductCategories
{
    public class GetProductCategoriesUseCase : IUseCase<GetProductCategoriesDAO, UseCaseOutput<IEnumerable<Domain.Entities.ProductCategory>>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductCategoriesUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<UseCaseOutput<IEnumerable<Domain.Entities.ProductCategory>>> Handle(GetProductCategoriesDAO input)
        {
            try
            {
                var ordersByStatus = await _productRepository.GetProductCategories();

                return new UseCaseOutput<IEnumerable<Domain.Entities.ProductCategory>>(ordersByStatus);
            }
            catch (Exception ex)
            {
                return new UseCaseOutput<IEnumerable<Domain.Entities.ProductCategory>>($"Erro ao buscar categorias de produtos na base de dados - {ex.Message}");
            }

        }
    }
}
