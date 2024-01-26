using FluentValidation;
using ToTable.Contract;

namespace ToTable.Validator;

public class RestaurantValidator : AbstractValidator<RestaurantDto>
{
    public RestaurantValidator()
    {
                RuleFor(x => x.Login).NotEmpty();
                RuleFor(x => x.TableQuantity).NotNull().GreaterThan(0);
                RuleFor(x => x.WaiterQantity).NotNull().GreaterThan(0);
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.RestaurantName).NotEmpty();

    }
}