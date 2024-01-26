using FluentValidation;
using ToTable.Contract;

namespace ToTable.Validator;

public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty();
        RuleFor(x => x.ProductDescription).NotEmpty();
        RuleFor(x => x.ProductPrice).NotNull().GreaterThan(0);
        RuleFor(x => x.ImageUrl).NotEmpty();
        RuleFor(x => x.ProductCategory).IsInEnum().WithMessage("Chose 0-MainCourse, 1-Soup, 2-Soup, 3-Soup, 4-Beverages, 5-Extras");
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0);

    }
}