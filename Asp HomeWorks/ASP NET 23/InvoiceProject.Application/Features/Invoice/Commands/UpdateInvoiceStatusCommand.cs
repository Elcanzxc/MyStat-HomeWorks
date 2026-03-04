using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Models;
using MediatR;

namespace InvoiceProject.Application.Features.Invoice.Commands;

public record UpdateInvoiceStatusCommand(int Id, InvoiceStatus Status, string UserId) : IRequest<bool>;

public class UpdateInvoiceStatusHandler : IRequestHandler<UpdateInvoiceStatusCommand, bool>
{
    private readonly IInvoiceRepository _repo;


    public UpdateInvoiceStatusHandler(IInvoiceRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(UpdateInvoiceStatusCommand request, CancellationToken ct)
    {
        var invoice = await _repo.GetById(request.Id, request.UserId);
        if (invoice == null) return false;

        invoice.Status = request.Status;
        return true;
    }
}