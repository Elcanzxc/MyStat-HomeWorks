using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO;
using InvoiceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InvoiceProject.Services;

public class InvoiceRowService : IInvoiceRowService
{
    private readonly IInvoiceRowRepository _repository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;
    private readonly string _currentUserId;

    public InvoiceRowService(
        IInvoiceRowRepository repository,
        IInvoiceRepository invoiceRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;

        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        _currentUserId = userId ?? throw new UnauthorizedAccessException("User not authenticated");
    }

    public async Task<IEnumerable<InvoiceRowResponseDto>> GetAll()
    {
        var rows = await _repository.GetAll(_currentUserId);
        return _mapper.Map<IEnumerable<InvoiceRowResponseDto>>(rows);
    }

    public async Task<InvoiceRowResponseDto> AddRow(int invoiceId, InvoiceRowRequestDto dto)
    {

        var invoice = await _invoiceRepository.GetById(invoiceId, _currentUserId);

        if (invoice == null) throw new KeyNotFoundException("Invoice not found or access denied.");

        if (invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot add rows to an invoice that is not in 'Created' status.");

        var rowEntity = _mapper.Map<InvoiceRow>(dto);
        rowEntity.InvoiceId = invoiceId;

        await _repository.AddRow(rowEntity);


        invoice.RecalculateTotal();

        
        return _mapper.Map<InvoiceRowResponseDto>(rowEntity);
    }

    public async Task<InvoiceRowResponseDto?> UpdateRow(int rowId, InvoiceRowUpdateDto dto)
    {
        var row = await _repository.GetByIdWithInvoice(rowId, _currentUserId);

        if (row == null) return null;

        if (row.Invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot update rows of an invoice that is not in 'Created' status.");

        _mapper.Map(dto, row);
        row.Invoice.RecalculateTotal();

        await _repository.UpdateRow();
        return _mapper.Map<InvoiceRowResponseDto>(row);
    }

    public async Task<bool> DeleteRow(int rowId)
    {
        var row = await _repository.GetByIdWithInvoice(rowId, _currentUserId);

        if (row == null) return false;

        if (row.Invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot delete rows from an invoice that is not in 'Created' status.");

        var invoice = row.Invoice;
        await _repository.Delete(row);


        invoice.RecalculateTotal();

        return true;
    }
}