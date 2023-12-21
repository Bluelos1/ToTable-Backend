namespace ToTable.Models;

public class Payment
{
    public int PayId { get; set; }
    public int PayCost { get; set; }
    public string PayMethod { get; set; }
    public string PayStatus { get; set; }
    public Order Order { get; set; }
}