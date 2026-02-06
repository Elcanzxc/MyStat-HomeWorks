using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO.Customer;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceProject.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("Invoice/json")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(customers);
    }


    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResponseDto>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }


    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<CustomerResponseDto>> Create([FromBody] CustomerRequestDto dto)
    {
        var result = await _customerService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResponseDto>> Update(int id, [FromBody] CustomerRequestDto dto)
    {
        var result = await _customerService.UpdateAsync(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }


    [HttpDelete("{id:int}/archive")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var success = await _customerService.SoftDeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }


    [HttpDelete("{id:int}/hard-delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> HardDelete(int id)
    {
        try
        {
            var success = await _customerService.HardDeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
         
            return BadRequest(new { message = ex.Message });
        }
    }
}
