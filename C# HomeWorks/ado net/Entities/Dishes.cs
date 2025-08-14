using ado_net.Interfaces;

namespace ado_net.Entities;
class Dishes : IPrintable
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Price { get; set; }

    public override string ToString() => $"""
        ========= Dish Info =========
        Id    : {Id}
        Name  : {Name}
        Price : {Price} AZN
        =============================
        """;
}
