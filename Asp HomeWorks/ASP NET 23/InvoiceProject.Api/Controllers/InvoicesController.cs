using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Application.Features.Invoice.Commands;
using InvoiceProject.Application.Features.Invoice.Query;
using InvoiceProject.Common;
using InvoiceProject.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InvoiceProject.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoicesController(IMediator mediator) => _mediator = mediator;

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> GetById(int id)
    {
        var result = await _mediator.Send(new GetInvoiceByIdQuery(id, UserId));
        return result != null
            ? Ok(ApiResponse<InvoiceResponseDto>.SuccessResponse(result, "Invoice found"))
            : NotFound(ApiResponse<InvoiceResponseDto>.ErrorResponse("Invoice not found"));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> Create([FromBody] InvoiceRequestDto dto)
    {
        var result = await _mediator.Send(new CreateInvoiceCommand(dto, UserId));
        return CreatedAtAction(nameof(GetById), new { id = result.Id },
            ApiResponse<InvoiceResponseDto>.SuccessResponse(result, "Created successfully"));
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateStatus(int id, [FromBody] InvoiceUpdateStatusDto dto)
    {
        var success = await _mediator.Send(new UpdateInvoiceStatusCommand(id, dto.Status, UserId));
        return success
            ? Ok(ApiResponse<bool>.SuccessResponse(true, "Status updated"))
            : NotFound(ApiResponse<bool>.ErrorResponse("Invoice not found"));
    }

    [HttpGet("{id}/download/pdf")]
    public async Task<IActionResult> DownloadPdf(int id)
    {
        var result = await _mediator.Send(new DownloadInvoicePdfQuery(id, UserId));
        if (result == null) return NotFound();

        return File(result.Value.Bytes, "application/pdf", result.Value.FileName);
    }

    [HttpGet("paged")]
    public async Task<ActionResult<ApiResponse<PagedResult<InvoiceResponseDto>>>> GetPaged([FromQuery] InvoiceQueryParams q)
    {
        var result = await _mediator.Send(new GetInvoicesPagedQuery(q, UserId));
        return Ok(ApiResponse<PagedResult<InvoiceResponseDto>>.SuccessResponse(result, "Success"));
    }

    
}