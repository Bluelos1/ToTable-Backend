namespace ToTable.Contract;

public class RestaurantDto
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string TableQuantity { get; set; }
    public string WaiterQantity { get; set; }
}