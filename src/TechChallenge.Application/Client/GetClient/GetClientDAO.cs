using TechChallenge.Application.Common.UseCase.Interfaces;

namespace TechChallenge.Application.Order.GetClient;

public class GetClientDAO: IUseCaseDAO
{
    public long Cpf { get; set; }

    public GetClientDAO(long cpf)
    {
        Cpf = cpf;
    }
}
