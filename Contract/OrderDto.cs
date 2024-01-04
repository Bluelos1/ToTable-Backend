namespace ToTable.Contract;

public class OrderDto
{
    public int OrderId { get; set; }
    public int OrderTime { get; set; }
    public string OrderStatus { get; set; }
    public string OrderComment { get; set; }
}