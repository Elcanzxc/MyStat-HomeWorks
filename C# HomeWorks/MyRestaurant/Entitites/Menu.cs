namespace MyRestaurant.Entitites;

public class Menu
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Price { get; set; }

    public string? Category { get; set; }

    public string? Description { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }

    public override string ToString()
    {
        return
$@"Id: {Id}
Food Name:{Name}
Food Price:{Price}
Category:{Category}
Description about food: {Description}
";
    }

}
