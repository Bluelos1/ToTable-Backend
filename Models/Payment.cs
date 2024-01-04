using System.ComponentModel.DataAnnotations;

namespace ToTable.Models;

public class Payment
{
    [Key]
    public int? PayId { get; set; }
    public int PayCost { get; set; }
    public string PayMethod { get; set; }
    public string PayStatus { get; set; }
}