using FluentValidation;
using TechChallenge.Application.Order.UpdateOrderStatus;
using TechChallenge.Domain.Enums;

public class UpdateOrderStatusDAOValidator : AbstractValidator<UpdateOrderStatusDAO>
{
    public UpdateOrderStatusDAOValidator()
    {
        RuleFor(x => x.OrderStatus).NotEqual(OrderStatus.ACTIVE).WithMessage($"{OrderStatus.ACTIVE} is a readonly status - represents ({OrderStatus.RECEIVED}, {OrderStatus.IN_PREPARATION} and {OrderStatus.READY})");
    }
}
