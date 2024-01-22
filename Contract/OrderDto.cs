using ToTable.Models;

namespace ToTable.Contract;

public class OrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderTime { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string? OrderComment { get; set; }
    public string? PaymentMethod { get; set; }

    public int? WaiterId { get; set; }
    public int TableId { get; set; }
    public int RestaurantId { get; set; }
}