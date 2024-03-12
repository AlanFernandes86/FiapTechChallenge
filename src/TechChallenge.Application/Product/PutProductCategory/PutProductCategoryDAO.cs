using TechChallenge.Application.Common.UseCase.Interfaces;
namespace TechChallenge.Application.Order.PutProductCategory;

public class PutProductCategoryDAO : IUseCaseDAO
{
    public int? Id { get; set; } = null;
    public string Name { get; set; }
}
