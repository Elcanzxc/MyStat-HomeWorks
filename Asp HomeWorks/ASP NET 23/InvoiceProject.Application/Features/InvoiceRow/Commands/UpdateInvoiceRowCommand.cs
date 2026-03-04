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

public record UpdateInvoiceRowCommand(int RowId, InvoiceRowUpdateDto Dto, string UserId) : IRequest<InvoiceRowResponseDto?>;

public class UpdateInvoiceRowHandler : IRequestHandler<UpdateInvoiceRowCommand, InvoiceRowResponseDto?>
{
    private readonly IInvoiceRowRepository _rowRepo;
    private readonly IMapper _mapper;

    public UpdateInvoiceRowHandler(IInvoiceRowRepository rowRepo, IMapper mapper)
    {
        _rowRepo = rowRepo;
        _mapper = mapper;
    }

    public async Task<InvoiceRowResponseDto?> Handle(UpdateInvoiceRowCommand request, CancellationToken ct)
    {
        var row = await _rowRepo.GetByIdWithInvoice(request.RowId, request.UserId);
        if (row == null) return null;

        if (row.Invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Invoice is locked for editing");

        _mapper.Map(request.Dto, row);
        row.Invoice.RecalculateTotal();

        return _mapper.Map<InvoiceRowResponseDto>(row);
    }
}