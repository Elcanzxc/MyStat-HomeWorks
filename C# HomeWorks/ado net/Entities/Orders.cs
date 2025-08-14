using ado_net.Interfaces;

namespace ado_net.Entities;

class Orders : IPrintable
{
    public int Id { get; set; }

    public required int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public override string ToString() => $"""
        =============== Order Info ===============
        OrderId    : {Id}
        CustomerId : {CustomerId}
        OrderDate  : {OrderDate:yyyy-MM-dd}
        ==========================================
        """;
}
