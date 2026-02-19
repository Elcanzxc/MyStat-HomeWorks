namespace InvoiceProject.DTO.Customer;

public class CustomerUpdateDto
{
    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }
}
