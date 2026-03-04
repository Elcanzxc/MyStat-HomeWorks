using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using InvoiceProject.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProject.Application.Features.InvoiceRow.Commands;

public record AddInvoiceRowCommand(int InvoiceId, InvoiceRowRequestDto Dto, string UserId) : IRequest<InvoiceRowResponseDto>;

public class AddInvoiceRowHandler : IRequestHandler<AddInvoiceRowCommand, InvoiceRowResponseDto>
{
    private readonly IInvoiceRowRepository _rowRepo;
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly IMapper _mapper;

    public AddInvoiceRowHandler(IInvoiceRowRepository rowRepo, IInvoiceRepository invoiceRepo, IMapper mapper)
    {
        _rowRepo = rowRepo;
        _invoiceRepo = invoiceRepo;
        _mapper = mapper;
    }

    public async Task<InvoiceRowResponseDto> Handle(AddInvoiceRowCommand request, CancellationToken ct)
    {
        var invoice = await _invoiceRepo.GetById(request.InvoiceId, request.UserId);
        if (invoice == null) throw new KeyNotFoundException("Invoice not found");

        if (invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot add rows to non-editable invoice");

        var row = _mapper.Map<InvoiceProject.Models.InvoiceRow>(request.Dto);
        row.InvoiceId = request.InvoiceId;

        await _rowRepo.AddRow(row);

        invoice.RecalculateTotal(); 


        return _mapper.Map<InvoiceRowResponseDto>(row);
    }
}