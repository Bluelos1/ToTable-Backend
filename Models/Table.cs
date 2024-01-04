using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Table
{
    [Key]
    public int TabId { get; set; }
    public int TabNum { get; set; }
    public bool TabStatus { get; set; }
}
