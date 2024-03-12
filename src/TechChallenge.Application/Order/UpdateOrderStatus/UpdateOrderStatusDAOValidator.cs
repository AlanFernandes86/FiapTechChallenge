using FluentValidation;
using TechChallenge.Domain.Enums;

namespace TechChallenge.Application.Order.UpdateOrderStatus;

public class UpdateOrderStatusDAOValidator : AbstractValidator<UpdateOrderStatusDAO>
{
    public UpdateOrderStatusDAOValidator()
    {
        RuleFor(x => x.OrderStatus).NotEqual(OrderStatus.ACTIVE).WithMessage($"{OrderStatus.ACTIVE} is a readonly status - represents ({OrderStatus.RECEIVED}, {OrderStatus.IN_PREPARATION} and {OrderStatus.READY})");
    }
}
