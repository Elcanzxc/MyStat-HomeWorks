using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Application.Features.InvoiceRow.Commands;
using InvoiceProject.Application.Features.InvoiceRow.Query;
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
public class InvoiceRowsController : ControllerBase
{
    private readonly IMediator _mediator;
    public InvoiceRowsController(IMediator mediator) => _mediator = mediator;

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<InvoiceRowResponseDto>>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllInvoiceRowsQuery(UserId));
        return Ok(ApiResponse<IEnumerable<InvoiceRowResponseDto>>.SuccessResponse(result, "Success"));
    }

    [HttpPost("invoice/{invoiceId:int}")]
    public async Task<ActionResult<ApiResponse<InvoiceRowResponseDto>>> AddRow(int invoiceId, [FromBody] InvoiceRowRequestDto dto)
    {
        var result = await _mediator.Send(new AddInvoiceRowCommand(invoiceId, dto, UserId));
        return Ok(ApiResponse<InvoiceRowResponseDto>.SuccessResponse(result, "Row added"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<InvoiceRowResponseDto>>> UpdateRow(int id, [FromBody] InvoiceRowUpdateDto dto)
    {
        var result = await _mediator.Send(new UpdateInvoiceRowCommand(id, dto, UserId));
        return result != null
            ? Ok(ApiResponse<InvoiceRowResponseDto>.SuccessResponse(result, "Row updated"))
            : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteRow(int id)
    {
        var success = await _mediator.Send(new DeleteInvoiceRowCommand(id, UserId));
        return success ? Ok(ApiResponse<bool>.SuccessResponse(true, "Deleted")) : NotFound();
    }
}