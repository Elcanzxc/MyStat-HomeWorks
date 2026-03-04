using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProject.Application.Features.InvoiceRow.Query;

public record GetAllInvoiceRowsQuery(string UserId) : IRequest<IEnumerable<InvoiceRowResponseDto>>;

public class GetAllInvoiceRowsHandler : IRequestHandler<GetAllInvoiceRowsQuery, IEnumerable<InvoiceRowResponseDto>>
{
    private readonly IInvoiceRowRepository _repo;
    private readonly IMapper _mapper;

    public GetAllInvoiceRowsHandler(IInvoiceRowRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InvoiceRowResponseDto>> Handle(GetAllInvoiceRowsQuery request, CancellationToken ct)
    {
        var rows = await _repo.GetAll(request.UserId);
        return _mapper.Map<IEnumerable<InvoiceRowResponseDto>>(rows);
    }
}