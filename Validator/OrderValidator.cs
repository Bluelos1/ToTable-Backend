using FluentValidation;
using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Validator;

public class OrderValidator : AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(x => x.OrderTime).NotNull();
        RuleFor(x => x.OrderStatus).IsInEnum().WithMessage("Put 0-New or 1-InProgress or 2-Done");
        RuleFor(x => x.WaiterId).GreaterThan(0);
        RuleFor(x => x.TableId).NotNull().GreaterThan(0);
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0);
    }
}