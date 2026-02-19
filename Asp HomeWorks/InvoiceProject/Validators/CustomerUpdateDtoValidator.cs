using FluentValidation;
using InvoiceProject.DTO.Customer;

namespace InvoiceProject.Validators;

public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
{
    public CustomerUpdateDtoValidator()
    {
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

       
        RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Address));

       
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("Phone number must contain 10–15 digits and may start with '+'.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}
