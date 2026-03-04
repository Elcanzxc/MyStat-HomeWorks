using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Application.Features.Customers.Commands;
using InvoiceProject.Application.Features.Customers.Queries;
using InvoiceProject.Common;
using InvoiceProject.DTO;
using InvoiceProject.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InvoiceProject.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator) => _mediator = mediator;

    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CustomerResponseDto>>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllCustomersQuery(CurrentUserId));
        return Ok(ApiResponse<IEnumerable<CustomerResponseDto>>.SuccessResponse(result, "Success"));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> GetById(int id)
    {
        var result = await _mediator.Send(new GetByIdQuery(id, CurrentUserId));
        return result != null
            ? Ok(ApiResponse<CustomerResponseDto>.SuccessResponse(result, "Found"))
            : NotFound(ApiResponse<CustomerResponseDto>.ErrorResponse("Not found"));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> Create([FromBody] CustomerRequestDto dto)
    {
        var result = await _mediator.Send(new CreateCustomerCommand(dto, CurrentUserId));
        return CreatedAtAction(nameof(GetById), new { id = result.Id },
            ApiResponse<CustomerResponseDto>.SuccessResponse(result, "Created"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> Update(int id, [FromBody] CustomerUpdateDto dto)
    {
        var result = await _mediator.Send(new UpdateCustomerCommand(id, dto, CurrentUserId));
        return result != null ? Ok(ApiResponse<CustomerResponseDto>.SuccessResponse(result, "Updated")) : NotFound();
    }

    [HttpDelete("{id:int}/archive")]
    public async Task<ActionResult<ApiResponse<bool>>> SoftDelete(int id)
    {
        var success = await _mediator.Send(new DeleteCustomerCommand(id, CurrentUserId, IsHardDelete: false));
        return success ? Ok(ApiResponse<bool>.SuccessResponse(true, "Archived")) : NotFound();
    }

    [HttpGet("paged")]
    public async Task<ActionResult<ApiResponse<PagedResult<CustomerResponseDto>>>> GetPaged([FromQuery] CustomerQueryParams queryParams)
    {
        var result = await _mediator.Send(new GetCustomerPagedQuery(queryParams, CurrentUserId));
        return Ok(ApiResponse<PagedResult<CustomerResponseDto>>.SuccessResponse(result, "Success"));
    }
}