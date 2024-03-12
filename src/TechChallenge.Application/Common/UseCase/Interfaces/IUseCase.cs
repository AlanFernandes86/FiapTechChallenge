namespace TechChallenge.Application.Common.UseCase.Interfaces;

public interface IUseCase<IUseCaseDAO, IUseCaseOutput>
{
    Task<IUseCaseOutput> Handle(IUseCaseDAO useCaseDAO);
}
