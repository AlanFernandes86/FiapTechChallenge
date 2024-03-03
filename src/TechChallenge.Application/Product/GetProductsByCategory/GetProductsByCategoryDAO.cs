using TechChallenge.Application.Common.UseCase.Interfaces;

namespace TechChallenge.Application.Order.GetProductsByCategory;

public class GetProductsByCategoryDAO: IUseCaseDAO
{
    public readonly int CategoryId;

    public GetProductsByCategoryDAO(int categoryId)
    {
        CategoryId = categoryId;
    }
}
