

namespace MyRestaurant.Entitites;
public class Order
{
    public int Id { get; set; }

    public  DateTime OrderTime { get; set; }

    public  string Status { get; set; }

    public required decimal TotalPrice { get; set; }

    public required Customer Customer { get; set; }

    public required Waiter Waiter { get; set; }

    public int CustomerId { get; set; }
    public int WaiterId { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }

}