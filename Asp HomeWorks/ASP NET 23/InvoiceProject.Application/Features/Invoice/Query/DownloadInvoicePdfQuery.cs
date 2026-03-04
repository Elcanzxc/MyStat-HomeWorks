using InvoiceProject.Abtractions.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProject.Application.Features.Invoice.Query;

public record DownloadInvoicePdfQuery(int Id, string UserId) : IRequest<(byte[] Bytes, string FileName)?>;

public class DownloadInvoicePdfHandler : IRequestHandler<DownloadInvoicePdfQuery, (byte[] Bytes, string FileName)?>
{
    private readonly IInvoiceExportService _exportService;

    public DownloadInvoicePdfHandler(IInvoiceExportService exportService) => _exportService = exportService;

    public async Task<(byte[] Bytes, string FileName)?> Handle(DownloadInvoicePdfQuery request, CancellationToken ct)
    {
        try
        {
            var bytes = await _exportService.GeneratePdfAsync(request.Id, request.UserId);
            return (bytes, $"Invoice_{request.Id}_{DateTime.Now:yyyyMMdd}.pdf");
        }
        catch (KeyNotFoundException) { return null; }
    }
}