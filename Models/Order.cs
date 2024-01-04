using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToTable.Models;
using System.ComponentModel.DataAnnotations.Schema;
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    public DateTime OrderTime { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string OrderComment { get; set; }
    public int WaiterId { get; set; }
    public Waiter Waiter { get; set; } 
    public int PaymentId { get; set; }
    public Payment Payment { get; set; }
    public int TableId { get; set; }
    public Table Table { get; set; }
    
}