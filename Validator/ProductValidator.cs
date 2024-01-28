using FluentValidation;
using ToTable.Contract;

namespace ToTable.Validator;

public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().Must(x => x is string).WithMessage("Must be string");
        RuleFor(x => x.ProductDescription).NotEmpty().Must(x => x is string).WithMessage("Must be string");
        RuleFor(x => x.ProductPrice).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("Must be int");
        RuleFor(x => x.ImageUrl).NotEmpty().Must(x => x is string).WithMessage("Must be string");
        RuleFor(x => x.ProductCategory).IsInEnum().WithMessage("Chose 0-MainCourse, 1-Soup, 2-Soup, 3-Soup, 4-Beverages, 5-Extras");
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("Must be int");
        RuleFor(x => x.ProductStatus).NotEmpty().Must(x => x is string).WithMessage("Must be string");

    }
}