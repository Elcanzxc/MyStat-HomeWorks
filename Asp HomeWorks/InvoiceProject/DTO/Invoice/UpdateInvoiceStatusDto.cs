using InvoiceProject.Models;

namespace InvoiceProject.DTO.Invoice;

public class UpdateInvoiceStatusDto
{
    public InvoiceStatus NewStatus { get; set; }
}
