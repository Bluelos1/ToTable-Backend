using FluentValidation;
using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Validator;

public class OrderValidator : AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(x => x.OrderTime).NotNull().WithMessage("OrderTime Must be DateTime");
        RuleFor(x => x.OrderStatus).IsInEnum().WithMessage("OrderStatus - 0-New or 1-InProgress or 2-Done");
        RuleFor(x => x.WaiterId).Must(x => x is int or null).WithMessage("WaiterId Must be int or null");
        RuleFor(x => x.TableId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("TableId Must be int");
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("RestaurantId Must be int");
        RuleFor(x => x.OrderComment).Must(x => x is string or null).WithMessage("OrderComment Must be string or null");
        RuleFor(x => x.PaymentMethod).Must(x => x is string or null).WithMessage("PaymentMethod Must be string or null");
        

        
    }
}