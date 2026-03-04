using InvoiceProject.Abtractions.Interfaces;
using MediatR;

namespace InvoiceProject.Application.Features.InvoiceRow.Commands;

public record DeleteInvoiceRowCommand(int Id, string UserId) : IRequest<bool>;

public class DeleteInvoiceRowHandler : IRequestHandler<DeleteInvoiceRowCommand, bool>
{
    private readonly IInvoiceRowRepository _rowRepo;

    public DeleteInvoiceRowHandler(IInvoiceRowRepository rowRepo)
    {
        _rowRepo = rowRepo;
    }

    public async Task<bool> Handle(DeleteInvoiceRowCommand request, CancellationToken ct)
    {
        var row = await _rowRepo.GetByIdWithInvoice(request.Id, request.UserId);
        if (row == null) return false;

        var invoice = row.Invoice;
        await _rowRepo.Delete(row);

        invoice.RecalculateTotal();

        return true;
    }
}