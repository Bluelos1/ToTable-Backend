using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToTable.Models;

public class OrderItem
{
    [Key]
    public int ItemId { get; set; }
    public int ItemQuantity { get; set; }
    public double ItemPrice { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
}