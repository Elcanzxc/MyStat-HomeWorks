using FluentValidation;
using InvoiceProject.DTO;
using InvoiceProject.Models;

namespace InvoiceProject.Validators;

public class InvoiceUpdateStatusDtoValidator : AbstractValidator<InvoiceUpdateStatusDto>
{
    public InvoiceUpdateStatusDtoValidator()
    {
       
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid status.");

      
        RuleFor(x => x)
            .Must(x => CanChangeStatus(x.Status))
            .WithMessage("Invalid status transition.");
    }


    private bool CanChangeStatus(InvoiceStatus status)
    {
     
        return status != InvoiceStatus.Paid; 
    }
}