using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using MediatR;


namespace InvoiceProject.Application.Features.Customers.Queries;

public record GetAllCustomersQuery(string UserId) : IRequest<IEnumerable<CustomerResponseDto>>;

public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerResponseDto>>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;

    public GetAllCustomersHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerResponseDto>> Handle(GetAllCustomersQuery request, CancellationToken ct)
    {
        var customers = await _repo.GetAll(request.UserId);
        return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
    }
}