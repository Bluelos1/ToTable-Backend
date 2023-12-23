using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ToTable.Models
{
    public class Table
    {
        [Key]
        public int TabId { get; set; }
        public int TabNum { get; set; }
        public bool TabStatus { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
