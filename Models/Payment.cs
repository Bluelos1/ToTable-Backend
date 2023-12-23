using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToTable.Models
{
    public class Payment
    {
        [Key]
        public int PayId { get; set; }
        public int PayCost { get; set; }
        public string PayMethod { get; set; }
        public string PayStatus { get; set; }

      
        public Order Order { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
    }
}
