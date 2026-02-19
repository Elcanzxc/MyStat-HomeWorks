using FluentValidation;
using InvoiceProject.DTO.Customer;

namespace InvoiceProject.Validators;


public class CustomerRequestDtoValidator : AbstractValidator<CustomerRequestDto>
{
    public CustomerRequestDtoValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        
        RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Address));

       
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");

       
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("Phone number must contain 10–15 digits and may start with '+'.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}