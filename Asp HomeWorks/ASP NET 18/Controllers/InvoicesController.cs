using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceProject.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceResponseDto>>>> GetAll()
    {
       
        var result = await _invoiceService.GetAll();
        return Ok(ApiResponse<IEnumerable<InvoiceResponseDto>>.SuccessResponse(result, "Invoices retrieved successfully"));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> GetById(int id)
    {
        var result = await _invoiceService.GetById(id);
        if (result == null)
            return NotFound(ApiResponse<InvoiceResponseDto>.ErrorResponse($"Invoice with ID {id} not found or access denied"));

        return Ok(ApiResponse<InvoiceResponseDto>.SuccessResponse(result, "Invoice found"));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> Create([FromBody] InvoiceRequestDto dto)
    {
      
        var result = await _invoiceService.Create(dto);
        var response = ApiResponse<InvoiceResponseDto>.SuccessResponse(result, "Invoice created successfully");

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> Update(int id, [FromBody] InvoiceUpdateDto dto)
    {
        var result = await _invoiceService.Update(id, dto);
        if (result == null)
            return NotFound(ApiResponse<InvoiceResponseDto>.ErrorResponse($"Invoice with ID {id} not found or access denied"));

        return Ok(ApiResponse<InvoiceResponseDto>.SuccessResponse(result, "Invoice updated successfully"));
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateStatus(int id, [FromBody] InvoiceUpdateStatusDto statusDto)
    {
        var success = await _invoiceService.UpdateStatus(id, statusDto.Status);
        if (!success)
            return NotFound(ApiResponse<bool>.ErrorResponse($"Invoice with ID {id} not found or access denied"));

        return Ok(ApiResponse<bool>.SuccessResponse(true, $"Status changed to {statusDto.Status}"));
    }

    [HttpDelete("{id:int}/archive")]
    public async Task<ActionResult<ApiResponse<bool>>> SoftDelete(int id)
    {
        var success = await _invoiceService.SoftDelete(id);
        if (!success)
            return NotFound(ApiResponse<bool>.ErrorResponse($"Invoice with ID {id} not found or access denied"));

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Invoice archived successfully"));
    }

    [HttpDelete("{id:int}/hard-delete")]
    public async Task<ActionResult<ApiResponse<bool>>> HardDelete(int id)
    {
        var success = await _invoiceService.HardDelete(id);
        if (!success)
            return NotFound(ApiResponse<bool>.ErrorResponse($"Invoice with ID {id} not found or access denied"));

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Invoice permanently deleted"));
    }

    [HttpGet("paged")]
    public async Task<ActionResult<ApiResponse<PagedResult<InvoiceResponseDto>>>> GetPaged([FromQuery] InvoiceQueryParams queryParams)
    {
        var result = await _invoiceService.GetPagedAsync(queryParams);
        return Ok(ApiResponse<PagedResult<InvoiceResponseDto>>.SuccessResponse(result, "Invoices retrieved successfully"));
    }
}