namespace ToTable.Contract;

public class OrderItemDto
{
    public int ItemId { get; set; }
    public int ItemQuantity { get; set; }
    public double ItemPrice { get; set; }
    public int ProductId { get; set; }
    public int? OrderId { get; set; }
}