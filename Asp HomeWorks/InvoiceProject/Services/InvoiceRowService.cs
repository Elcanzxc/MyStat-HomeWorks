using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.DataAccess.Entities;
using InvoiceProject.DTO.Invoice;
using Microsoft.EntityFrameworkCore;

namespace InvoiceProject.Services;

public class InvoiceRowService : IInvoiceRowService
{
    private readonly InvoiceDbContext _context;
    private readonly IMapper _mapper;

    public InvoiceRowService(InvoiceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InvoiceRowDto> AddRowAsync(int invoiceId, CreateInvoiceRowDto dto)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Rows)
            .FirstOrDefaultAsync(i => i.Id == invoiceId);

        if (invoice == null) throw new KeyNotFoundException("Invoice not found.");

    
        if (invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot add rows to an invoice that is not in 'Created' status.");

        var rowEntity = _mapper.Map<InvoiceRowEntity>(dto);
        rowEntity.InvoiceId = invoiceId;

        _context.InvoiceRows.Add(rowEntity);

      
        invoice.TotalSum = invoice.Rows.Sum(r => r.Sum) + rowEntity.Sum;

        await _context.SaveChangesAsync();
        return _mapper.Map<InvoiceRowDto>(rowEntity);
    }

    public async Task<InvoiceRowDto?> UpdateRowAsync(int rowId, CreateInvoiceRowDto dto)
    {
        var row = await _context.InvoiceRows
            .Include(r => r.Invoice)
            .ThenInclude(i => i.Rows)
            .FirstOrDefaultAsync(r => r.Id == rowId);

        if (row == null) return null;

        if (row.Invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot update rows of an invoice that is not in 'Created' status.");

       
        _mapper.Map(dto, row);

       
        row.Invoice.TotalSum = row.Invoice.Rows.Sum(r => r.Sum);

        await _context.SaveChangesAsync();
        return _mapper.Map<InvoiceRowDto>(row);
    }

    public async Task<bool> DeleteRowAsync(int rowId)
    {
        var row = await _context.InvoiceRows
            .Include(r => r.Invoice)
            .ThenInclude(i => i.Rows)
            .FirstOrDefaultAsync(r => r.Id == rowId);

        if (row == null) return false;

        if (row.Invoice.Status != InvoiceStatus.Created)
            throw new InvalidOperationException("Cannot delete rows from an invoice that is not in 'Created' status.");

        _context.InvoiceRows.Remove(row);

      
        row.Invoice.TotalSum = row.Invoice.Rows.Where(r => r.Id != rowId).Sum(r => r.Sum);

        await _context.SaveChangesAsync();
        return true;
    }
}