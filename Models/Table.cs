namespace ToTable.Models;

public class Table
{
    public int TabId { get; set; }
    public int TabNum { get; set; }
    public bool TabStatus { get; set; }
    public  ICollection<Order> Orders { get; set; }
}