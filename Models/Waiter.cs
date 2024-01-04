using System.ComponentModel.DataAnnotations;

namespace ToTable.Models;

public class Waiter
{
    [Key]
    public int WaiterId { get; set; }
    public string WaiterName { get; set; }
    public int WaiterSuma { get; set; }
    public string WaiterLogin { get; set; }
    public string WaiterPassw { get; set; }
    public bool IsAvailable { get; set; }
}