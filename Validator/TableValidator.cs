using FluentValidation;
using ToTable.Contract;

namespace ToTable.Validator;

public class TableValidator : AbstractValidator<TableDto>
{
    public TableValidator()
    {
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0);
        RuleFor(x => x.TabStatus).NotEmpty();
        RuleFor(x => x.TabNum).NotNull().GreaterThan(0); 

    }
}