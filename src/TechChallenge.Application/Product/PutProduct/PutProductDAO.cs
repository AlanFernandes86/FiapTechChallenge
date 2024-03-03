using TechChallenge.Application.Common.UseCase.Interfaces;

namespace TechChallenge.Application.Order.PutProduct;

public class PutProductDAO: IUseCaseDAO
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public int CategoryId { get; set; }
}
