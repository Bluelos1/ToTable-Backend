namespace ToTable.Contract;

public class PaymentDto
{
    public int PayId { get; set; }
    public int PayCost { get; set; }
    public string PayMethod { get; set; }
    public string PayStatus { get; set; }
}