using FluentValidation;
using InvoiceProject.DTO.ApplicationUserDto;

namespace InvoiceProject.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MinimumLength(2).WithMessage("First name should be at least 2 characters long.")
            .Matches(@"^[\p{L}]+( [\p{L}]+)*$").WithMessage("First name can only contain letters, no numbers, spaces, or special characters allowed.");


        RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name should be at least 2 characters long.")
                .Matches(@"^[\p{L}]+( [\p{L}]+)*$").WithMessage("Last name can only contain letters, no numbers, spaces, or special characters allowed.");


        RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .Matches(@"^\S+$").WithMessage("Email must not contain spaces.");


        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Passwords must be at least 6 characters.,Passwords must have at least one digit ('0'-'9').,Passwords must have at least one lowercase ('a'-'z').,Passwords must have at least one uppercase ('A'-'Z')");

        RuleFor(x => x.ConfirmedPassword)
            .NotEmpty().WithMessage("Confirmed Password required")
            .Equal(x => x.Password).WithMessage("Passwords do not match");

    }
}




public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .Matches(@"^\S+$").WithMessage("Email must not contain spaces.");


        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Passwords must be at least 6 characters.,Passwords must have at least one digit ('0'-'9').,Passwords must have at least one lowercase ('a'-'z').,Passwords must have at least one uppercase ('A'-'Z')");
    }
}
