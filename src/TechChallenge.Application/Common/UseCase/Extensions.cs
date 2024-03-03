using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;

namespace TechChallenge.Application.Common.UseCase.Extensions
{
    public static class Extensions
    {
        public static IActionResult ToResult<T>(this IUseCaseOutput<T> useCaseOutput, ControllerBase controller)
        {
            if (useCaseOutput.OutputStatus == OutputStatus.Success)
            {
                return controller.Ok(useCaseOutput);
            }

            return controller.StatusCode(StatusCodes.Status500InternalServerError, useCaseOutput);
        }
    }
}
