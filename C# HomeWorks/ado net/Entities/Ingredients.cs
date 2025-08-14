using ado_net.Interfaces;

namespace ado_net.Entities;

class Ingredients : IPrintable
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public override string ToString() => $"""
        ========= Ingredient Info =========
        Id   : {Id}
        Name : {Name}
        ==================================
        """;

}
