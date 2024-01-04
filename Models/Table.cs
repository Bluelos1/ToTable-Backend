using System.ComponentModel.DataAnnotations;

namespace ToTable.Models;

public class Table
{
    [Key]
    public int TabId { get; set; }
    public int TabNum { get; set; }
    public bool TabStatus { get; set; }
}