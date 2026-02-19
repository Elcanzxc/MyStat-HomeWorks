namespace InvoiceProject.DTO.Customer;

public class CustomerRequestDto
{
    
    public string Name { get; set; } = null!;
    
    public string? Address { get; set; }
  
    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

}
