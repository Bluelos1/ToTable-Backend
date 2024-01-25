namespace ToTable.Contract;

public class RestaurantDto
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public int TableQuantity { get; set; }
    public int WaiterQantity { get; set; }
}