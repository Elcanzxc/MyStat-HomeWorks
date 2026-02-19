using FluentValidation;
using InvoiceProject.DTO.Invoice;

namespace InvoiceProject.Validators;

public class InvoiceRowRequestDtoValidator : AbstractValidator<InvoiceRowRequestDto>
{
    public InvoiceRowRequestDtoValidator()
    {
      
        RuleFor(x => x.Service)
            .NotEmpty()
            .WithMessage("Service is required.")
            .MaximumLength(200)
            .WithMessage("Service name must not exceed 200 characters.");

        
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");

        
        RuleFor(x => x.Rate)
            .GreaterThan(0)
            .WithMessage("Rate must be greater than 0.");
    }
}
