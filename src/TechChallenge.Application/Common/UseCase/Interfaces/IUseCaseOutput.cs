using TechChallenge.Application.Common.UseCase.Models;

namespace TechChallenge.Application.Common.UseCase.Interfaces;

internal interface IUseCaseOutput<T>
{
    public OutputStatus OutputStatus { get; }
    public T? Data { get; }
    public string? ErrorMessage { get; }
}
