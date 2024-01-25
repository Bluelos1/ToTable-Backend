namespace ToTable.Models;

public class Restaurant
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public int TableQuantity { get; set; }
    public int WaiterQantity { get; set; }
    
    public ICollection<Table> Tables { get; set; }
    public ICollection<Waiter> Waiters { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Product> Products { get; set; }
    
}