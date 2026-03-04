using InvoiceProject.DTO;
using MediatR;


namespace InvoiceProject.Application.Features.Customers.Queries;

public record GetByIdQuery(int Id, string userId) : IRequest<CustomerResponseDto?>;
