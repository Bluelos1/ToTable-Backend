using FluentValidation;
using ToTable.Contract;

namespace ToTable.Validator;

public class WaiterValidator : AbstractValidator<WaiterDto>
{
    public WaiterValidator()
    {
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("RestaurantId Must be int");
        RuleFor(x => x.IsAdmin).NotNull().WithMessage("IsAdmin Must be boolean");;
        RuleFor(x => x.IsAvailable).NotNull().WithMessage("IsAvailable Must be boolean");;
        RuleFor(x => x.WaiterName).NotEmpty().Must(x => x is string).WithMessage("WaiterName Must be string");
        RuleFor(x => x.WaiterLogin).NotEmpty().Must(x => x is string).WithMessage("WaiterLogin Must be string");
        RuleFor(x => x.WaiterPassw).NotEmpty().Must(x => x is string).WithMessage("WaiterPassw Must be string");
        RuleFor(x => x.WaiterSurname).NotEmpty().Must(x => x is string).WithMessage("WaiterSurname Must be string");

    }
}