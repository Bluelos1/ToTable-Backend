namespace ToTable.Models;

public class Order
{
    public int OrderId { get; set; }
    public int OrderTime { get; set; }
    public string OrderStatus { get; set; }
    public string OrderComment { get; set; }
    public Waiter Waiter { get; set; } 
    public Payment Payment { get; set; }
    public Table Table { get; set; }
    
}