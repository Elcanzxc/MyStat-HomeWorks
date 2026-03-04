using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using MediatR;

namespace InvoiceProject.Application.Features.Invoice.Commands;

public record CreateInvoiceCommand(InvoiceRequestDto Dto, string UserId) : IRequest<InvoiceResponseDto>;

public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, InvoiceResponseDto>
{
    private readonly IInvoiceRepository _repo;
    private readonly IMapper _mapper;

    public CreateInvoiceHandler(IInvoiceRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<InvoiceResponseDto> Handle(CreateInvoiceCommand request, CancellationToken ct)
    {
        if (!await _repo.CustomerExists(request.Dto.CustomerId, request.UserId))
            throw new UnauthorizedAccessException("Customer not found or access denied.");

        var invoice = _mapper.Map<InvoiceProject.Models.Invoice>(request.Dto);
        invoice.RecalculateTotal();

        await _repo.Create(invoice);

        return _mapper.Map<InvoiceResponseDto>(invoice);
    }
}