using FluentValidation;
using InvoiceProject.DTO;

namespace InvoiceProject.Validators;


public class InvoiceRequestDtoValidator : AbstractValidator<InvoiceRequestDto>
{
    public InvoiceRequestDtoValidator()
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

     
        RuleFor(x => x.Comment)
            .MaximumLength(500)
            .WithMessage("Comment must not exceed 500 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Comment));

      
        RuleFor(x => x.RowsCreate)
            .NotNull()
            .WithMessage("Rows are required.")
            .Must(x => x.Any())
            .WithMessage("Invoice must contain at least one row.");

       
        RuleForEach(x => x.RowsCreate)
            .SetValidator(new InvoiceRowRequestDtoValidator());
    }
}
