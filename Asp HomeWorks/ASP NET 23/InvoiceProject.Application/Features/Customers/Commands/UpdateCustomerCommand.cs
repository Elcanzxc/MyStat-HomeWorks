using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProject.Application.Features.Customers.Commands;

public record UpdateCustomerCommand(int Id, CustomerUpdateDto Dto, string UserId) : IRequest<CustomerResponseDto?>;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponseDto?>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CustomerResponseDto?> Handle(UpdateCustomerCommand request, CancellationToken ct)
    {
        var customer = await _repo.GetById(request.Id, request.UserId);
        if (customer == null) return null;

        _mapper.Map(request.Dto, customer);
        customer.UpdatedAt = DateTimeOffset.UtcNow;

        await _repo.Update(customer);
        return _mapper.Map<CustomerResponseDto>(customer);
    }
}