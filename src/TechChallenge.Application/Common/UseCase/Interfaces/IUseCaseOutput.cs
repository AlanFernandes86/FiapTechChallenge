using TechChallenge.Application.Common.UseCase.Models;

namespace TechChallenge.Application.Common.UseCase.Interfaces;

public interface IUseCaseOutput<T>
{
    public OutputStatus OutputStatus { get; }
    public T? Data { get; }
    public string? Error { get; }
    public Validation? Validation { get; }
}


