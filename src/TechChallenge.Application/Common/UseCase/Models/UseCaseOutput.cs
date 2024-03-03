using TechChallenge.Application.Common.UseCase.Interfaces;

namespace TechChallenge.Application.Common.UseCase.Models
{
    public class UseCaseOutput<T> : IUseCaseOutput<T>
    {
        public OutputStatus OutputStatus { get; }

        public T? Data { get; }

        public string? Error { get; }

        public Validation? Validation { get; }

        public UseCaseOutput(T data)
        {
            OutputStatus = OutputStatus.Success;
            Data = data;
        }

        public UseCaseOutput(string? errorMessage)
        {
            OutputStatus = OutputStatus.Error;
            Error = errorMessage;
        }

        public UseCaseOutput(Validation? validationMessage)
        {
            OutputStatus = OutputStatus.Validation;
            Validation = validationMessage;
        }
    }
}
