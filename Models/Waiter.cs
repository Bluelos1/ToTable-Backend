using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
