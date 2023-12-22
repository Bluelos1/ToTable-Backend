using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace ToTable.Models;
[Table("Fruits")]
public class OrderItem
{
    [Key]
    public int ItemId { get; set; }
    public int ItemQuantity { get; set; }
    public int ItemPrice { get; set; }
    public Product Product { get; set; }
    public Order Order { get; set; }
}