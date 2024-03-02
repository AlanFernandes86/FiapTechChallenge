using TechChallenge.Application.Common.UseCase.Interfaces;

namespace TechChallenge.Application.Common.UseCase.Models
{
    public class UseCaseOutput<T> : IUseCaseOutput<T>
    {
        public OutputStatus OutputStatus { get; }

        public T? Data { get; }

        public string? ErrorMessage { get; }

        public UseCaseOutput(T data)
        {
            OutputStatus = OutputStatus.Success;
            Data = data;
        }

        public UseCaseOutput(string? errorMessage)
        {
            OutputStatus = OutputStatus.Error;
            ErrorMessage = errorMessage;
        }
    }
}
