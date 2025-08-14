using ado_net.Interfaces;

namespace ado_net.Entities;

class OrdersDetailed : IPrintable
{
    public int Id { get; set; }

    public Customers? Customer { get; set; }

    public DateTime OrderDate { get; set; }

    public override string ToString() => $"""
        ========= Order With Customer =========
        CustomerId : {Customer.Id}
        FullName   : {Customer.FullName}
        Phone      : {Customer.Phone ?? "N/A"}
        OrderId    : {Id}
        OrderDate  : {OrderDate:yyyy-MM-dd}
        =======================================
        """;

}
