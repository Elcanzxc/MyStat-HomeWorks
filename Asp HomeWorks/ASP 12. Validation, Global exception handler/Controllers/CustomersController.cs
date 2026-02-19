using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO.Customer;
using InvoiceProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceProject.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CustomerResponseDto>>>> GetAll()
    {
        var customers = await _customerService.GetAll();
        return Ok(ApiResponse<IEnumerable<CustomerResponseDto>>.SuccessResponse(customers, "Returns the list of Customers successfully"));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> GetById(int id)
    {
        var customer = await _customerService.GetById(id);
        if (customer == null)
            return NotFound(ApiResponse<CustomerResponseDto>.ErrorResponse($"Customer with ID {id} not found"));

        return Ok(ApiResponse<CustomerResponseDto>.SuccessResponse(customer, "Customer found"));
    }


    [HttpGet("/detailed")]
    public async Task<ActionResult<ApiResponse<IEnumerable<CustomerDetailsResponseDto>>>> GetAllDetailed()
    {
        var customers = await _customerService.GetAllDetailed();
        return Ok(ApiResponse<IEnumerable<CustomerDetailsResponseDto>>.SuccessResponse(customers, "Returns the list of Customers successfully"));
    }

    [HttpGet("{id:int}/detailed")]
    public async Task<ActionResult<ApiResponse<CustomerDetailsResponseDto>>> GetDetailedById(int id)
    {
        var customer = await _customerService.GetDetailedById(id);
        if (customer == null)
            return NotFound(ApiResponse<CustomerDetailsResponseDto>.ErrorResponse($"Customer with ID {id} not found"));

        return Ok(ApiResponse<CustomerDetailsResponseDto>.SuccessResponse(customer, "Customer found"));
    }



    [HttpPost]
    public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> Create([FromBody] CustomerRequestDto dto)
    {
        var result = await _customerService.Create(dto);
        var response = ApiResponse<CustomerResponseDto>.SuccessResponse(result, "Customer created successfully");

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> Update(int id, [FromBody] CustomerUpdateDto dto)
    {
        var result = await _customerService.Update(id, dto);
        if (result == null)
            return NotFound(ApiResponse<CustomerResponseDto>.ErrorResponse($"Customer with ID {id} not found"));

        return Ok(ApiResponse<CustomerResponseDto>.SuccessResponse(result, "Customer updated successfully"));
    }


    [HttpDelete("{id:int}/archive")]
    public async Task<ActionResult<ApiResponse<bool>>> SoftDelete(int id)
    {
        var success = await _customerService.SoftDeleteAsync(id);
        if (!success)
            return NotFound(ApiResponse<bool>.ErrorResponse($"Customer with ID {id} not found"));

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Customer archived successfully"));
    }

    [HttpDelete("{id:int}/hard-delete")]
    public async Task<ActionResult<ApiResponse<bool>>> HardDelete(int id)
    {
        var success = await _customerService.HardDeleteAsync(id);
        if (!success)
            return NotFound(ApiResponse<bool>.ErrorResponse($"Customer with ID {id} not found"));

        return Ok(ApiResponse<bool>.SuccessResponse(true, "Customer permanently deleted"));

    }

    /// <summary>
    /// Gets a paginated list of customers with search and sort options.
    /// </summary>
    [HttpGet("paged")]
    public async Task<ActionResult<ApiResponse<PagedResult<CustomerResponseDto>>>> GetAll([FromQuery] CustomerQueryParams queryParams)
    {
        var result = await _customerService.GetPagedAsync(queryParams);
        return Ok(ApiResponse<PagedResult<CustomerResponseDto>>.SuccessResponse(result, "Customers retrieved successfully"));
    }
}
