using FluentValidation;
using ToTable.Contract;

namespace ToTable.Validator;

public class TableValidator : AbstractValidator<TableDto>
{
    public TableValidator()
    {
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("RestaurantId Must be int");
        RuleFor(x => x.TabStatus).NotEmpty().WithMessage("TabStatus Must be boolean");
        RuleFor(x => x.TabNum).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("TabNum Must be int"); 

    }
}