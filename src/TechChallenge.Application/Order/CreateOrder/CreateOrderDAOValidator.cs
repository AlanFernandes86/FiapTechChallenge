using FluentValidation;
using TechChallenge.Application.Common.UseCase.Extensions;

namespace TechChallenge.Application.Order.CreateOrder;
public class CreateOrderDAOValidator : AbstractValidator<CreateOrderDAO>
{
    public CreateOrderDAOValidator()
    {
        RuleFor(x => x.ClientName).NotEmpty().When(x => string.IsNullOrEmpty(x.ClientCpf))
            .WithMessage("O campo 'ClientName' não pode ser vazio quando 'cpf' está vazio.");

        RuleFor(x => x.ClientCpf).NotEmpty().When(x => string.IsNullOrEmpty(x.ClientName))
            .WithMessage("O campo 'ClientCpf' não pode ser vazio quando 'ClientName' está vazio.")
            .CpfValido();

        RuleForEach(x => x.Products).SetValidator(new ProductOnOrderDAOValidator());
    }
}

public class ProductOnOrderDAOValidator : AbstractValidator<ProductOnOrderDAO>
{
    public ProductOnOrderDAOValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O campo 'ProductId' não pode ser 0.")
            .NotNull().WithMessage("O campo 'ProductId' não pode ser nulo.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("O campo 'Price' não pode ser 0.")
            .NotNull().WithMessage("O campo 'Price' não pode ser nulo.");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("O campo 'Quantity' não pode ser 0.")
            .NotNull().WithMessage("O campo 'Quantity' não pode ser nulo.");
    }
}
