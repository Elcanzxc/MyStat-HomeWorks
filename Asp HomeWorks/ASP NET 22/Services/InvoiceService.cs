using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DataAccess;
using InvoiceProject.DTO;
using InvoiceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Security.Claims;

namespace InvoiceProject.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly string _currentUserId;

    public InvoiceService(
        IInvoiceRepository repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _mapper = mapper;

        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        _currentUserId = userId ?? throw new UnauthorizedAccessException("User not authenticated");
    }

    public async Task<IEnumerable<InvoiceResponseDto>> GetAll()
    {
        var invoices = await _repository.GetAll(_currentUserId);
        return _mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices);
    }

    public async Task<InvoiceResponseDto?> GetById(int id)
    {
        var invoice = await _repository.GetById(id, _currentUserId);
        return invoice == null ? null : _mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<InvoiceResponseDto> Create(InvoiceRequestDto dto)
    {
 
        if (!await _repository.CustomerExists(dto.CustomerId, _currentUserId))
        {
            throw new UnauthorizedAccessException("You cannot create an invoice for this customer.");
        }

        var invoice = _mapper.Map<Invoice>(dto);

        invoice.RecalculateTotal(); 

        await _repository.Create(invoice);
       

        return _mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<InvoiceResponseDto?> Update(int id, InvoiceUpdateDto dto)
    {
        var invoice = await _repository.GetById(id, _currentUserId);
        if (invoice == null) return null;

        
        if (invoice.Status != InvoiceStatus.Created)
        {
            throw new InvalidOperationException("Only invoices with 'Created' status can be edited.");
        }

        _mapper.Map(dto, invoice);
        invoice.RecalculateTotal();

        _repository.Update(invoice);
     

        return _mapper.Map<InvoiceResponseDto>(invoice);
    }

    public async Task<bool> UpdateStatus(int id, InvoiceStatus newStatus)
    {
        var entity = await _repository.GetById(id, _currentUserId);
        if (entity == null) return false;

        await _repository.UpdateStatus(id, newStatus ,_currentUserId);
        return true;
    }

    public async Task<bool> SoftDelete(int id)
    {
        var entity = await _repository.GetById(id, _currentUserId);
        if (entity == null) return false;

        await _repository.Delete(entity);
        return true;
    }

    public async Task<bool> HardDelete(int id)
    {
        var entity = await _repository.GetById(id, _currentUserId);
        if (entity == null) return false;

        if (entity.Status != InvoiceStatus.Created)
        {
            throw new InvalidOperationException("Only invoices with 'Created' status can be hard deleted.");
        }

        await _repository.Delete(entity);
        return true;
    }

    public async Task<PagedResult<InvoiceResponseDto>> GetPagedAsync(InvoiceQueryParams queryParams)
    {
        queryParams.Validate();

        var (items, totalCount) = await _repository.GetPagedAsync(
            _currentUserId,
            queryParams.CustomerId,
            queryParams.Status,
            queryParams.MinTotal,
            queryParams.Search,
            queryParams.Sort,
            queryParams.SortDirection!,
            queryParams.Page,
            queryParams.PageSize);

        var dtos = _mapper.Map<IEnumerable<InvoiceResponseDto>>(items);

        return PagedResult<InvoiceResponseDto>.Create(dtos, queryParams.Page, queryParams.PageSize, totalCount);
    }
}