using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.DataAccess.Entities;
using InvoiceProject.DTO.Customer;
using Microsoft.EntityFrameworkCore;

namespace InvoiceProject.Services;

public class CustomerService : ICustomerService
{
    private readonly InvoiceDbContext _context;
    private readonly IMapper _mapper;

    public CustomerService(InvoiceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
    }

    public async Task<CustomerResponseDto?> GetByIdAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        return customer == null ? null : _mapper.Map<CustomerResponseDto>(customer);
    }

    public async Task<CustomerResponseDto> CreateAsync(CustomerRequestDto dto)
    {
        var entity = _mapper.Map<CustomerEntity>(dto);

        _context.Customers.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerResponseDto>(entity);
    }

    public async Task<CustomerResponseDto?> UpdateAsync(int id, CustomerRequestDto dto)
    {
        var entity = await _context.Customers.FindAsync(id);
        if (entity == null) return null;

        _mapper.Map(dto, entity);

        await _context.SaveChangesAsync();
        return _mapper.Map<CustomerResponseDto>(entity);
    }


    public async Task<bool> SoftDeleteAsync(int id)
    {
        var entity = await _context.Customers.FindAsync(id);
        if (entity == null) return false;

        entity.DeletedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> HardDeleteAsync(int id)
    {
        var entity = await _context.Customers
            .Include(c => c.Invoices)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (entity == null) return false;

        if (entity.Invoices.Any())
        {
            throw new InvalidOperationException("Cannot hard delete customer with existing invoices.");
        }

        _context.Customers.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
