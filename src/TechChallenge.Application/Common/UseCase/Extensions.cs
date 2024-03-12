using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;

namespace TechChallenge.Application.Common.UseCase.Extensions
{
    public static class Extensions
    {
        public static IActionResult ToActionResult<T>(this IUseCaseOutput<T> useCaseOutput, ControllerBase controller)
        {
            if (useCaseOutput.OutputStatus == OutputStatus.Success)
            {
                return controller.Ok(useCaseOutput);
            }

            if (useCaseOutput.OutputStatus == OutputStatus.Validation)
            {
                return controller.BadRequest(useCaseOutput);
            }

            return controller.StatusCode(StatusCodes.Status500InternalServerError, useCaseOutput);
        }

        public static IRuleBuilderInitial<T, string> CpfValido<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return (IRuleBuilderInitial<T, string>)ruleBuilder.Custom((cpf, context) =>
            {
                if (string.IsNullOrWhiteSpace(cpf))
                {
                    return;
                }

                Span<int> cpfArray = stackalloc int[11];
                var count = 0;
                foreach (var c in cpf)
                {
                    if (!char.IsDigit(c))
                    {
                        context.AddFailure($"'{context.DisplayName}' tem que ser numérico.");
                        return;
                    }


                    if (char.IsDigit(c))
                    {
                        if (count > 10)
                        {
                            context.AddFailure($"'{context.DisplayName}' deve possuir 11 caracteres. Foram informados " + cpf.Length);
                            return;
                        }


                        cpfArray[count] = c - '0';
                        count++;
                    }
                }

                if (count != 11)
                {
                    context.AddFailure($"'{context.DisplayName}' deve possuir 11 caracteres. Foram informados " + cpf.Length);
                    return;
                }
                if (VerificarTodosValoresSaoIguais(ref cpfArray))
                {
                    context.AddFailure($"'{context.DisplayName}' Não pode conter todos os dígitos iguais.");
                    return;
                }

                var totalDigitoI = 0;
                var totalDigitoII = 0;
                int modI;
                int modII;

                for (var posicao = 0; posicao < cpfArray.Length - 2; posicao++)
                {
                    totalDigitoI += cpfArray[posicao] * (10 - posicao);
                    totalDigitoII += cpfArray[posicao] * (11 - posicao);
                }

                modI = totalDigitoI % 11;
                if (modI < 2) { modI = 0; }
                else { modI = 11 - modI; }

                if (cpfArray[9] != modI)
                {
                    context.AddFailure($"'{context.DisplayName}' Inválido.");
                    return;
                }

                totalDigitoII += modI * 2;

                modII = totalDigitoII % 11;
                if (modII < 2) { modII = 0; }
                else { modII = 11 - modII; }

                return;

            });
        }

        static bool VerificarTodosValoresSaoIguais(ref Span<int> input)
        {
            for (var i = 1; i < 11; i++)
            {
                if (input[i] != input[0])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
