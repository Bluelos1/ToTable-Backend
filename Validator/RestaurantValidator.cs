using FluentValidation;
using ToTable.Contract;

namespace ToTable.Validator;

public class RestaurantValidator : AbstractValidator<RestaurantDto>
{
    public RestaurantValidator()
    {
                RuleFor(x => x.Login).NotEmpty().Must(x => x is string).WithMessage("Login Must be string");
                RuleFor(x => x.TableQuantity).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("TableQuantity Must be int");
                RuleFor(x => x.WaiterQantity).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("WaiterQantity Must be int");
                RuleFor(x => x.Password).NotEmpty().Must(x => x is string).WithMessage("Password Must be string");
                RuleFor(x => x.RestaurantName).NotEmpty().Must(x => x is string).WithMessage("RestaurantName Must be string");

    }
}