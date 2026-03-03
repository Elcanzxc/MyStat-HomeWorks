using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InvoiceProject.Services;

public class InvoiceExportService : IInvoiceExportService
{
    private readonly IInvoiceRepository _repository;

    public InvoiceExportService(IInvoiceRepository repository)
    {
        _repository = repository;
      
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<byte[]> GeneratePdfAsync(int invoiceId, string userId)
    {
        var invoice = await _repository.GetById(invoiceId, userId);
        if (invoice == null) throw new KeyNotFoundException("Invoice not found");

     
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Text($"INVOICE #{invoice.Id}").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                page.Content().Column(col =>
                {
                    col.Item().PaddingVertical(10).LineHorizontal(1);
                    col.Item().Text($"Customer: {invoice.Customer.Name}");
                    col.Item().Text($"Date: {invoice.CreatedAt:dd.MM.yyyy}");

                    col.Item().PaddingTop(20).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Description");
                            header.Cell().AlignRight().Text("Quantity");
                            header.Cell().AlignRight().Text("Price");
                        });

                        foreach (var row in invoice.Rows)
                        {
                            table.Cell().Text(row.Service);
                            table.Cell().AlignRight().Text(row.Quantity.ToString());
                            table.Cell().AlignRight().Text($"{row.Rate}$");
                            table.Cell().AlignRight().Text($"{row.Sum}$");
                        }
                    });

                    col.Item().AlignRight().PaddingTop(20)
                        .Text($"Total: {invoice.TotalSum}$").FontSize(16).Bold();
                });
            });
        });

        return document.GeneratePdf();
    }
}
