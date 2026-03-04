using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO;
using MediatR;


namespace InvoiceProject.Application.Features.Customers.Queries;

public record GetCustomerPagedQuery(CustomerQueryParams Params, string UserId) : IRequest<PagedResult<CustomerResponseDto>>;

public class GetCustomerPagedHandler : IRequestHandler<GetCustomerPagedQuery, PagedResult<CustomerResponseDto>>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;

    public GetCustomerPagedHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PagedResult<CustomerResponseDto>> Handle(GetCustomerPagedQuery request, CancellationToken ct)
    {
        var p = request.Params;
        var (items, total) = await _repo.GetPagedAsync(request.UserId, p.Search, p.Sort, p.SortDirection ?? "asc", p.Page, p.PageSize);

        var dtos = _mapper.Map<IEnumerable<CustomerResponseDto>>(items);
        return PagedResult<CustomerResponseDto>.Create(dtos, p.Page, p.PageSize, total);
    }
}