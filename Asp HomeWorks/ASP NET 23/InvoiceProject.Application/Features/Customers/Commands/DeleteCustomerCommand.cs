using InvoiceProject.Abtractions.Interfaces;
using MediatR;


namespace InvoiceProject.Application.Features.Customers.Commands;

public record DeleteCustomerCommand(int Id, string UserId, bool IsHardDelete = false) : IRequest<bool>;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly ICustomerRepository _repo;

    public DeleteCustomerHandler(ICustomerRepository repo) => _repo = repo;

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken ct)
    {
        var customer = await _repo.GetById(request.Id, request.UserId);
        if (customer == null) return false;

        if (request.IsHardDelete)
        {
            await _repo.Delete(customer);
        }
        else
        {
            customer.DeletedAt = DateTimeOffset.UtcNow;
            await _repo.Update(customer);
        }

        return true;
    }
}