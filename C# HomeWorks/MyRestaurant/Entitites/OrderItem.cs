namespace MyRestaurant.Entitites;

public class OrderItem
{

    public int Id { get; set; }



    public int Quantity { get; set; }

    public decimal PriceAtOrder { get; set; }

    public required Order Order { get; set; }

    public required Menu Menu { get; set; }

    public int OrderId { get; set; }

    public int MenuId { get; set; }

}
