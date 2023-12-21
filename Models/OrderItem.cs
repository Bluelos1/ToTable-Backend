using System.Net.Http.Headers;

namespace ToTable.Models;

public class OrderItem
{
    public int ItemId { get; set; }
    public int ItemQuantity { get; set; }
    public int ItemPrice { get; set; }
    public Product Product { get; set; }
    
}