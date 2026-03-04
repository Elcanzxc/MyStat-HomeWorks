using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProject.Application.Features.Invoice.Query;

public record GetInvoiceByIdQuery(int Id, string UserId) : IRequest<InvoiceResponseDto?>;

public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceResponseDto?>
{
    private readonly IInvoiceRepository _repo;
    private readonly IMapper _mapper;

    public GetInvoiceByIdHandler(IInvoiceRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<InvoiceResponseDto?> Handle(GetInvoiceByIdQuery request, CancellationToken ct)
    {
        var invoice = await _repo.GetById(request.Id, request.UserId);
        return _mapper.Map<InvoiceResponseDto?>(invoice);
    }
}