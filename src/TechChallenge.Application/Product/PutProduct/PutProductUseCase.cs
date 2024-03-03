using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Order.PutProduct
{
    public class PutProductUseCase : IUseCase<PutProductDAO, UseCaseOutput<int>>
    {
        private readonly IProductRepository _productRepository;

        public PutProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<UseCaseOutput<int>> Handle(PutProductDAO input)
        {
            try
            {
                var product = new Product
                {
                    Id = input.Id,
                    Name = input.Name,
                    Description = input.Description,
                    Price = input.Price,
                    CategoryId = input.CategoryId
                };

                var newProductId = await _productRepository.PutProduct(product);

                return new UseCaseOutput<int>(newProductId);
            }
            catch (Exception ex)
            {
                var id = input.Id != null ? $" id: [{input.Id}] " : string.Empty;
                var updateOrCreate = input.Id != null ? "Atualizar" : "Cadastrar";
                return new UseCaseOutput<int>($"Erro ao {updateOrCreate} produto [{input.Name}]{id}- {ex.Message}");
            }
 
        }
    }
}
