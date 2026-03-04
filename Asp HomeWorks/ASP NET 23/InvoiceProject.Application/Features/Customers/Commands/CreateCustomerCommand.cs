using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using InvoiceProject.Models;
using MediatR;


namespace InvoiceProject.Application.Features.Customers.Commands;

public record CreateCustomerCommand(CustomerRequestDto Dto, string UserId) : IRequest<CustomerResponseDto>;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerResponseDto>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CustomerResponseDto> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        var customer = _mapper.Map<Customer>(request.Dto);
        customer.UserId = request.UserId; 


        var result = await _repo.Create(customer);
        return _mapper.Map<CustomerResponseDto>(result);
    }
}