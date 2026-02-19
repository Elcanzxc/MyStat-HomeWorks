using FluentValidation;
using InvoiceProject.DTO.Invoice;

namespace InvoiceProject.Validators;

public class InvoiceUpdateDtoValidator : AbstractValidator<InvoiceUpdateDto>
{
    public InvoiceUpdateDtoValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("CustomerId must be greater than 0.");

   
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(DateTimeOffset.UtcNow)
            .WithMessage("StartDate cannot be in the future.");


        RuleFor(x => x.EndDate)
            .LessThanOrEqualTo(DateTimeOffset.UtcNow)
            .WithMessage("EndDate cannot be in the future.");

       
        RuleFor(x => x)
            .Must(x => x.EndDate >= x.StartDate)
            .WithMessage("EndDate must be greater than or equal to StartDate.");

        RuleFor(x => x)
            .Must(x => (x.EndDate - x.StartDate).TotalDays <= 365)
            .WithMessage("Invoice period cannot exceed 1 year.");


        RuleFor(x => x.Comment)
            .MaximumLength(500)
            .WithMessage("Comment must not exceed 500 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Comment));
    }
}
