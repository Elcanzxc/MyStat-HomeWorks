namespace InvoiceProject.DTO;

public class InvoiceRowRequestDto
{

    public string Service { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal Rate { get; set; }
}
public class InvoiceRowResponseDto
{

    public string Service { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal Rate { get; set; }

    public decimal Sum { get; set; }

}
public class InvoiceRowUpdateDto
{
    public string Service { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal Rate { get; set; }
}
