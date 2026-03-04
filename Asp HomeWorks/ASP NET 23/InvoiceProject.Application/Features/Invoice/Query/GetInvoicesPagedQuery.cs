using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProject.Application.Features.Invoice.Query;

public record GetInvoicesPagedQuery(InvoiceQueryParams Params, string UserId) : IRequest<PagedResult<InvoiceResponseDto>>;

public class GetInvoicesPagedHandler : IRequestHandler<GetInvoicesPagedQuery, PagedResult<InvoiceResponseDto>>
{
    private readonly IInvoiceRepository _repo;
    private readonly IMapper _mapper;

    public GetInvoicesPagedHandler(IInvoiceRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PagedResult<InvoiceResponseDto>> Handle(GetInvoicesPagedQuery request, CancellationToken ct)
    {
        var p = request.Params;
        var (items, total) = await _repo.GetPagedAsync(
            request.UserId, p.CustomerId, p.Status, p.MinTotal,
            p.Search, p.Sort, p.SortDirection ?? "desc", p.Page, p.PageSize);

        var dtos = _mapper.Map<IEnumerable<InvoiceResponseDto>>(items);
        return PagedResult<InvoiceResponseDto>.Create(dtos, p.Page, p.PageSize, total);
    }
}