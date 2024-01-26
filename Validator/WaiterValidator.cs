using FluentValidation;
using ToTable.Contract;

namespace ToTable.Validator;

public class WaiterValidator : AbstractValidator<WaiterDto>
{
    public WaiterValidator()
    {
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0);
        RuleFor(x => x.IsAdmin).NotEmpty();
        RuleFor(x => x.IsAvailable).NotNull();
        RuleFor(x => x.WaiterName).NotEmpty();
        RuleFor(x => x.WaiterLogin).NotEmpty();
        RuleFor(x => x.WaiterPassw).NotEmpty();
        RuleFor(x => x.WaiterSurname).NotEmpty();

    }
}