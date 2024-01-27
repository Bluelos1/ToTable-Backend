using FluentValidation;
using ToTable.Contract;


namespace ToTable.Validator;

public class OrderItemValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.ItemQuantity).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("Must be int");
        RuleFor(x => x.OrderId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("Must be int");
        RuleFor(x => x.ProductId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("Must be int");
    }
}
