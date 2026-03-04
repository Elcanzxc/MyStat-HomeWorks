using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO;
using InvoiceProject.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace InvoiceProject.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly string _currentUserId;

    public CustomerService(ICustomerRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _mapper = mapper;

     
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        _currentUserId = userId ?? throw new UnauthorizedAccessException("User ID not found in token.");
    }

    public async Task<IEnumerable<CustomerResponseDto>> GetAll()
    {
        var customers = await _repository.GetAll(_currentUserId);

        return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
    }

    public async Task<IEnumerable<CustomerDetailsResponseDto>> GetAllDetailed()
    {
        var customers = await _repository.GetAllDetailed(_currentUserId);

        return _mapper.Map<IEnumerable<CustomerDetailsResponseDto>>(customers);
    }

    public async Task<CustomerDetailsResponseDto?> GetDetailedById(int id)
    {
        var customer = await _repository.GetDetailedById(id, _currentUserId);

        return customer == null ? null : _mapper.Map<CustomerDetailsResponseDto>(customer);
    }

    public async Task<CustomerResponseDto?> GetById(int id)
    {
        var customer = await _repository.GetById(id, _currentUserId);

        return customer == null ? null : _mapper.Map<CustomerResponseDto>(customer);
    }

    public async Task<CustomerResponseDto> Create(CustomerRequestDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);

        
        entity.UserId = _currentUserId;
    

        await _repository.Create(entity);


        return _mapper.Map<CustomerResponseDto>(entity);
    }

    public async Task<CustomerResponseDto?> Update(int id, CustomerUpdateDto dto)
    {
        var entity = await _repository.GetById(id, _currentUserId);

        if (entity == null) return null;

        _mapper.Map(dto, entity);
    

        await _repository.Update(entity);

        return _mapper.Map<CustomerResponseDto>(entity);
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var entity = await _repository.GetById(id, _currentUserId);

        if (entity == null) return false;

        entity.DeletedAt = DateTimeOffset.UtcNow;
        
        await _repository.Delete(entity);

        return true;
    }

    public async Task<bool> HardDeleteAsync(int id)
    {
        var entity = await _repository.GetDetailedById(id, _currentUserId);

        if (entity == null) return false;

        bool hasSentInvoices = entity.Invoices.Any(i => i.Status != InvoiceStatus.Created);

        if (hasSentInvoices)
        {
            throw new InvalidOperationException("Cannot hard delete customer who has been sent at least one invoice.");
        }

        await _repository.Delete(entity);
     
        return true;
    }

    public async Task<PagedResult<CustomerResponseDto>> GetPagedAsync(CustomerQueryParams queryParams)
    {
  
        queryParams.Validate();


        var (customers, totalCount) = await _repository.GetPagedAsync(
            _currentUserId,
            queryParams.Search,
            queryParams.Sort,
            queryParams.SortDirection!,
            queryParams.Page,
            queryParams.PageSize);

   
        var dtos = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);

     
        return PagedResult<CustomerResponseDto>.Create(
            dtos,
            queryParams.Page,
            queryParams.PageSize,
            totalCount);
    }
}