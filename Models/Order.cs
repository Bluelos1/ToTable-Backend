namespace ToTable.Models;
using System.ComponentModel.DataAnnotations.Schema;
public class Order
{
 public int OrderId { get; set; }
        public int OrderTime { get; set; }
        public string OrderStatus { get; set; }
        public string OrderComment { get; set; }

        [ForeignKey("WaiterId")]
        public int WaiterId { get; set; }
        public Waiter Waiter { get; set; }

        [ForeignKey("PaymentId")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        [ForeignKey("TableId")]
        public int TableId { get; set; }
        public Table Table { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
}