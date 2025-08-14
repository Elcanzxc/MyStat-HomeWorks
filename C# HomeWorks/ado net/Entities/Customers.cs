using ado_net.Interfaces;
namespace ado_net.Entities;

class Customers:IPrintable
{
    public int Id { get; set; }

    public required string FullName { get; set; }

    public string? Phone { get; set; }

    public override string ToString() => $"""
    ============= Customer Info ==============
    Id           : {Id}
    Full Name    : {FullName}
    Phone Number : {Phone ?? "N/A"}
    ==========================================
    """;

}
