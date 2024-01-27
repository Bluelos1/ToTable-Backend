using FluentValidation;
using ToTable.Contract;

namespace ToTable.Validator;

public class WaiterValidator : AbstractValidator<WaiterDto>
{
    public WaiterValidator()
    {
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("Must be int");
        RuleFor(x => x.IsAdmin).NotEmpty();
        RuleFor(x => x.IsAvailable).NotNull();
        RuleFor(x => x.WaiterName).NotEmpty().Must(x => x is string).WithMessage("Must be string");
        RuleFor(x => x.WaiterLogin).NotEmpty().Must(x => x is string).WithMessage("Must be string");
        RuleFor(x => x.WaiterPassw).NotEmpty().Must(x => x is string).WithMessage("Must be string");
        RuleFor(x => x.WaiterSurname).NotEmpty().Must(x => x is string).WithMessage("Must be string");

    }
}