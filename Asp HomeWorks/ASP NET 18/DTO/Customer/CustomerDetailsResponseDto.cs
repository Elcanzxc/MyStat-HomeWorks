namespace InvoiceProject.DTO.Customer;
using InvoiceProject.DTO.Invoice;

public class CustomerDetailsResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public IEnumerable<InvoiceResponseDto> CustomerInvoices { get; set; } = null!;
    
}


