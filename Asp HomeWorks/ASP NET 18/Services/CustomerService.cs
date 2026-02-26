using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DataAccess;
using InvoiceProject.DTO.Customer;
using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace InvoiceProject.Services;

public class CustomerService : ICustomerService
{
    private readonly InvoiceDbContext _context;
    private readonly IMapper _mapper;
    private readonly string _currentUserId;

    public CustomerService(InvoiceDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;

        // Извлекаем ID пользователя из JWT токена
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        _currentUserId = userId ?? throw new UnauthorizedAccessException("User ID not found in token.");
    }

    public async Task<IEnumerable<CustomerResponseDto>> GetAll()
    {
        var customers = await _context.Customers
                    .Where(c => c.UserId == _currentUserId) // Фильтр
                    .ToListAsync();

        return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
    }

    public async Task<IEnumerable<CustomerDetailsResponseDto>> GetAllDetailed()
    {
        var customers = await _context.Customers
        .Include(c => c.Invoices)
            .ThenInclude(i => i.Rows)
        .Where(c => c.UserId == _currentUserId) // Фильтр
        .AsNoTracking()
        .ToListAsync();

        return _mapper.Map<IEnumerable<CustomerDetailsResponseDto>>(customers);
    }

    public async Task<CustomerDetailsResponseDto?> GetDetailedById(int id)
    {
        var customer = await _context.Customers
                .Include(c => c.Invoices)
                    .ThenInclude(i => i.Rows)
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == _currentUserId); // Фильтр

        return customer == null ? null : _mapper.Map<CustomerDetailsResponseDto>(customer);
    }

    public async Task<CustomerResponseDto?> GetById(int id)
    {
        var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == id && c.UserId == _currentUserId); // Фильтр
        return customer == null ? null : _mapper.Map<CustomerResponseDto>(customer);
    }

    public async Task<CustomerResponseDto> Create(CustomerRequestDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);

        // Принудительно привязываем клиента к текущему юзеру
        entity.UserId = _currentUserId;
        entity.CreatedAt = DateTimeOffset.UtcNow;

        _context.Customers.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerResponseDto>(entity);
    }

    public async Task<CustomerResponseDto?> Update(int id, CustomerUpdateDto dto)
    {
        var entity = await _context.Customers
           .FirstOrDefaultAsync(c => c.Id == id && c.UserId == _currentUserId); // Фильтр

        if (entity == null) return null;

        _mapper.Map(dto, entity);
        entity.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync();
        return _mapper.Map<CustomerResponseDto>(entity);
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var entity = await _context.Customers
               .FirstOrDefaultAsync(c => c.Id == id && c.UserId == _currentUserId); // Фильтр

        if (entity == null) return false;

        entity.DeletedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HardDeleteAsync(int id)
    {
        var entity = await _context.Customers
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == _currentUserId); // Фильтр

        if (entity == null) return false;

        bool hasSentInvoices = entity.Invoices.Any(i => i.Status != InvoiceStatus.Created);

        if (hasSentInvoices)
        {
            throw new InvalidOperationException("Cannot hard delete customer who has been sent at least one invoice.");
        }

        _context.Customers.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<PagedResult<CustomerResponseDto>> GetPagedAsync(CustomerQueryParams queryParams)
    {
        queryParams.Validate();

        var query = _context.Customers
            .Where(c => c.DeletedAt == null && c.UserId == _currentUserId) // Фильтр владельца
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var searchTerm = queryParams.Search.ToLower();
            query = query.Where(c =>
                c.Name.ToLower().Contains(searchTerm) ||
                c.Email.ToLower().Contains(searchTerm) ||
                (c.PhoneNumber != null && c.PhoneNumber.Contains(searchTerm)));
        }

        query = ApplySorting(query, queryParams.Sort, queryParams.SortDirection!);

        var totalCount = await query.CountAsync();
        var skip = (queryParams.Page - 1) * queryParams.PageSize;

        var customers = await query
            .Skip(skip)
            .Take(queryParams.PageSize)
            .ToListAsync();

        var dtos = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);

        return PagedResult<CustomerResponseDto>.Create(dtos, queryParams.Page, queryParams.PageSize, totalCount);
    }

    private IQueryable<Customer> ApplySorting(IQueryable<Customer> query, string? sort, string sortDirection)
    {
        var isDescending = sortDirection == "desc";

        return sort switch
        {
            "name" => isDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
            "email" => isDescending ? query.OrderByDescending(c => c.Email) : query.OrderBy(c => c.Email),
            "createdat" => isDescending ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt),
            _ => query.OrderBy(c => c.Name)
        };
    }
}