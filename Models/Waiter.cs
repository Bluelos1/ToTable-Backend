using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Web;

namespace ToTable.Models;

public class Waiter
{
    [Key]
    public int WaiterId { get; set; }
    public string WaiterName { get; set; }
    public string WaiterSurname { get; set; }
    public string WaiterLogin { get; set; }
    public string WaiterPassw { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsAdmin { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}