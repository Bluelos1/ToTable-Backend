using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToTable.Models
{
    [Table("Fruits")]
    public class OrderItem
    {
        [Key]
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemPrice { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
