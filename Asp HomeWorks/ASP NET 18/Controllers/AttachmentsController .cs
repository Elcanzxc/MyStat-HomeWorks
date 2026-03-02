using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO;
using InvoiceProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InvoiceProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttachmentsController : ControllerBase
{
    private readonly IInvoiceAttachmentService _invoiceAttachmentService;
    private readonly IInvoiceService _invoiceService;
    private readonly IInvoiceRowService _invoiceRowService;
    private readonly IAuthorizationService _authorizationService;

    public AttachmentsController(IInvoiceAttachmentService invoiceAttachmentService, IInvoiceService invoiceService, IInvoiceRowService invoiceRowService, IAuthorizationService authorizationService)
    {
        _invoiceAttachmentService = invoiceAttachmentService;
        _invoiceService = invoiceService;
        _invoiceRowService = invoiceRowService;
        _authorizationService = authorizationService;
    }


    //public AttachmentsController(
    //    ITaskAttachmentService taskAttachmentService,
    //    IProjectService projectService,
    //    IAuthorizationService authorizationService,
    //    ITaskItemService taskItemService)
    //{
    //    _taskAttachmentService = taskAttachmentService;
    //    _projectService = projectService;
    //    _authorizationService = authorizationService;
    //    _taskItemService = taskItemService;
    //}

    [HttpPost("~/api/invoice/{invoiceId}/attachments")]
    public async Task<ActionResult<ApiResponse<AttachmentResponseDto>>> UploadAsync(
        int userId,
        int invoiceId,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var invoice = await _invoiceService.GetById(invoiceId);
        if (invoice is null)
            return NotFound();

        var invoiceRows = await _invoiceRowService.GetAll();
        if (invoiceRows is null)
            return NotFound();


        if (file is null || file.Length == 0)
            return BadRequest();

        await using var stream = file.OpenReadStream();
        var dto = await _invoiceAttachmentService.UploadAsync(invoiceId, stream, file.FileName, file.ContentType, file.Length, userId, cancellationToken);
        if (dto is null)
            return NotFound();

        return Ok(ApiResponse<AttachmentResponseDto>.SuccessResponse(dto, "File uplaoded"));
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(int id, CancellationToken cancellationToken)
    {
        var info = await _invoiceAttachmentService.GetAttachmentInfoAsync(id, cancellationToken);
        if (info is null)
            return NotFound();

        var invoiceRows = await _invoiceRowService.GetAll();
        if (invoiceRows is null)
            return NotFound();



        var result = await _invoiceAttachmentService.GetDownloadAsync(id, cancellationToken);
        if (result is null)
            return NotFound();

        return File(result.Value.stream, result.Value.contentType, result.Value.fileName);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var info = await _invoiceAttachmentService.GetAttachmentInfoAsync(id, cancellationToken);
        if (info is null)
            return NotFound();

        var invoiceRows = await _invoiceRowService.GetAll();
        if (invoiceRows is null)
            return NotFound();

        var deleted = await _invoiceAttachmentService.DeleteAsync(id, cancellationToken);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
