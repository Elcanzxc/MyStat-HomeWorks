using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO.Invoice;
using InvoiceProject.DTO.InvoiceRow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceProject.Controllers;

[Authorize] // Глобально закрываем контроллер для безопасности
[ApiController]
[Route("api/[controller]")]
public class InvoiceRowsController : ControllerBase
{
    private readonly IInvoiceRowService _rowService;

    public InvoiceRowsController(IInvoiceRowService rowService)
    {
        _rowService = rowService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceRowResponseDto>>>> GetAll()
    {
        // Сервис автоматически отфильтрует только те строки, которые принадлежат клиентам юзера
        var result = await _rowService.GetAll();
        return Ok(ApiResponse<IEnumerable<InvoiceRowResponseDto>>.SuccessResponse(result, "Invoices retrieved successfully"));
    }

    [HttpPost("invoice/{invoiceId:int}")]
    public async Task<ActionResult<ApiResponse<InvoiceRowResponseDto>>> AddRow(int invoiceId, [FromBody] InvoiceRowRequestDto dto)
    {
        // Сервис проверит, принадлежит ли этот invoiceId текущему пользователю
        var result = await _rowService.AddRow(invoiceId, dto);
        return Ok(ApiResponse<InvoiceRowResponseDto>.SuccessResponse(result, "Row added and Invoice total updated"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<InvoiceRowResponseDto>>> UpdateRow(int id, [FromBody] InvoiceRowUpdateDto dto)
    {
        var result = await _rowService.UpdateRow(id, dto);
        if (result == null)
            return NotFound(ApiResponse<InvoiceRowResponseDto>.ErrorResponse($"Invoice row with ID {id} not found or access denied"));

        return Ok(ApiResponse<InvoiceRowResponseDto>.SuccessResponse(result, "Row updated and Invoice total recalculated"));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteRow(int id)
    {
        var success = await _rowService.DeleteRow(id);
        if (!success)
            return NotFound(ApiResponse<bool>.ErrorResponse($"Invoice row with ID {id} not found or access denied"));

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Row deleted and Invoice total recalculated"));
    }
}