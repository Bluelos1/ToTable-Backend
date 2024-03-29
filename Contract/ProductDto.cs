using ToTable.Models;

namespace ToTable.Contract;

public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public double ProductPrice { get; set; }
    public string ProductStatus { get; set; }
    public ProductCategoryEnum ProductCategory { get; set; }

    public string ImageUrl { get; set; }
    public int RestaurantId { get; set; }
}
