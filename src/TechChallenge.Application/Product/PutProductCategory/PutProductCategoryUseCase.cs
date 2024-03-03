using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.PutProductCategory
{
    public class PutProductCategoryUseCase : IUseCase<PutProductCategoryDAO, UseCaseOutput<int>>
    {
        private readonly IProductRepository _productRepository;

        public PutProductCategoryUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<UseCaseOutput<int>> Handle(PutProductCategoryDAO input)
        {
            try
            {
                var productCategory = new ProductCategory
                {
                    Id = input.Id,
                    Name = input.Name
                };

                var newProductCategoryId = await _productRepository.PutProductCategory(productCategory);

                return new UseCaseOutput<int>(newProductCategoryId);
            }
            catch (Exception ex)
            {
                var id = input.Id != null ? $" id: [{input.Id}] " : string.Empty;
                var updateOrCreate = input.Id != null ? "Atualizar" : "Cadastrar";
                return new UseCaseOutput<int>($"Erro ao {updateOrCreate} categoria [{input.Name}]{id}- {ex.Message}");
            }

        }
    }
}
