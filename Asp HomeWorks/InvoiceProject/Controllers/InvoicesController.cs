using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceProject.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("Invoice/json")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InvoiceResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InvoiceResponseDto>>> GetAll()
    {
        var result = await _invoiceService.GetAllAsync();
        return Ok(result);
    }

  
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(InvoiceResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceResponseDto>> GetById(int id)
    {
        var result = await _invoiceService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

  
    [HttpPost]
    [ProducesResponseType(typeof(InvoiceResponseDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<InvoiceResponseDto>> Create([FromBody] CreateInvoiceRequestDto dto)
    {
        var result = await _invoiceService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

   
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(InvoiceResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceResponseDto>> Update(int id, [FromBody] CreateInvoiceRequestDto dto)
    {
        try
        {
            var result = await _invoiceService.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

  
    [HttpPatch("{id:int}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateInvoiceStatusDto statusDto)
    {
        var success = await _invoiceService.UpdateStatusAsync(id, statusDto.NewStatus);
        if (!success) return NotFound();
        return NoContent();
    }

   
    [HttpDelete("{id:int}/archive")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var success = await _invoiceService.SoftDeleteAsync(id);
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
            var success = await _invoiceService.HardDeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
