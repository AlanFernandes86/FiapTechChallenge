using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.PutClient;

public class PutClientDAO: IUseCaseDAO
{
    public long Cpf { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
