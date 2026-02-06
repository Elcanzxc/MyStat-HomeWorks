using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceProject.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class InvoiceRowsController : ControllerBase
{
    private readonly IInvoiceRowService _rowService;

    public InvoiceRowsController(IInvoiceRowService rowService)
    {
        _rowService = rowService;
    }


    [HttpPost("invoice/{invoiceId:int}")]
    [ProducesResponseType(typeof(InvoiceRowDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceRowDto>> AddRow(int invoiceId, [FromBody] CreateInvoiceRowDto dto)
    {
        try
        {
            var result = await _rowService.AddRowAsync(invoiceId, dto);
            return Created("", result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Invoice not found" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

  
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(InvoiceRowDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceRowDto>> UpdateRow(int id, [FromBody] CreateInvoiceRowDto dto)
    {
        try
        {
            var result = await _rowService.UpdateRowAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

  
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRow(int id)
    {
        try
        {
            var success = await _rowService.DeleteRowAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}