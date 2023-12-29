namespace ToTable.Contract;

public class OrderItemDto
{
    public int OrderItemId { get; set; }
    public int ItemQuantity { get; set; }
    //public int ItemPrice { get; set; }
    public int ProductId { get; set; }
}