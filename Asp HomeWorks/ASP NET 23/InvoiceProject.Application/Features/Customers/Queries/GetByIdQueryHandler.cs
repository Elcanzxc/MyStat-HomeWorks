using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using MediatR;

namespace InvoiceProject.Application.Features.Customers.Queries;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, CustomerResponseDto?>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerResponseDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetById(request.Id, request.userId);

        if (customer == null) return null;

        return _mapper.Map<CustomerResponseDto>(customer);
    }
}
