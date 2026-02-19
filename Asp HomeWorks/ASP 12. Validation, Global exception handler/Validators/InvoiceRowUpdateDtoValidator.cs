using FluentValidation;
using InvoiceProject.DTO.InvoiceRow;

namespace InvoiceProject.Validators;

public class InvoiceRowUpdateDtoValidator : AbstractValidator<InvoiceRowUpdateDto>
{
    public InvoiceRowUpdateDtoValidator()
    {
   
        RuleFor(x => x.Service)
            .NotEmpty().WithMessage("Service is required.")
            .MaximumLength(200).WithMessage("Service name must not exceed 200 characters.");

       
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

      
        RuleFor(x => x.Rate)
            .GreaterThan(0).WithMessage("Rate must be greater than 0.");
    }
}