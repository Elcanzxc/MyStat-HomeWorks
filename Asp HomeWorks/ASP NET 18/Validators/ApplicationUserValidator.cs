using FluentValidation;
using InvoiceProject.DTO.ApplicationUserDto;

namespace InvoiceProject.Validators;

public class RegisterRequestValidator : AbstractValidator<UserRegister>
{
    public RegisterRequestValidator()
    {

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MinimumLength(2).WithMessage("First name should be at least 2 characters long.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.")
            .Matches(@"^[\p{L}]+( [\p{L}]+)*$")
            .WithMessage("First name can only contain letters and single spaces between words.")
            .Must(name => name.Trim() == name)
            .WithMessage("First name must not contain leading or trailing spaces.");


        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(2).WithMessage("Last name should be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
            .Matches(@"^[\p{L}]+( [\p{L}]+)*$")
            .WithMessage("Last name can only contain letters and single spaces between words.")
            .Must(name => name.Trim() == name)
            .WithMessage("Last name must not contain leading or trailing spaces.");

       
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .Matches(@"^\S+$").WithMessage("Email must not contain spaces.")
            .Must(email => email.Trim() == email)
            .WithMessage("Email must not contain leading or trailing spaces.");

    
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MinimumLength(5).WithMessage("Address must be at least 5 characters long.")
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.")
            .Must(address => address.Trim() == address)
            .WithMessage("Address must not contain leading or trailing spaces.");


        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{7,14}$")
            .WithMessage("Phone number must be valid and in international format (e.g. +1234567890).");

       
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");


        RuleFor(x => x.ConfirmedPassword)
            .NotEmpty().WithMessage("Confirmed password required.")
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match.");

    }
}




public class LoginRequestValidator : AbstractValidator<UserLogin>
{
    public LoginRequestValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .Matches(@"^\S+$").WithMessage("Email must not contain spaces.")
            .Must(email => email.Trim() == email)
            .WithMessage("Email must not contain leading or trailing spaces.");


        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
            .Must(password => password.Trim() == password)
            .WithMessage("Password must not contain leading or trailing spaces.");
    }
}

public class UserUpdateValidator : AbstractValidator<UserUpdate>
{
    public UserUpdateValidator()
    {

      
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.")
            .Matches(@"^[\p{L}]+( [\p{L}]+)*$")
            .WithMessage("First name can only contain letters and single spaces between words.")
            .Must(x => x.Trim() == x)
            .WithMessage("First name must not contain leading or trailing spaces.");

       
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
            .Matches(@"^[\p{L}]+( [\p{L}]+)*$")
            .WithMessage("Last name can only contain letters and single spaces between words.")
            .Must(x => x.Trim() == x)
            .WithMessage("Last name must not contain leading or trailing spaces.");

    
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MinimumLength(5).WithMessage("Address must be at least 5 characters long.")
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters.")
            .Must(x => x.Trim() == x)
            .WithMessage("Address must not contain leading or trailing spaces.");


        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{7,14}$")
            .WithMessage("Phone number must be valid and in international format (e.g. +1234567890).");
    }
}


public class UserPasswordUpdateValidator : AbstractValidator<UserPasswordUpdate>
{
    public UserPasswordUpdateValidator()
    {
     
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Cursrent password is required.")
            .MinimumLength(6).WithMessage("Current password must be at least 6 characters long.")
            .MaximumLength(100).WithMessage("Current password must not exceed 100 characters.")
            .Must(x => x.Trim() == x)
            .WithMessage("Current password must not contain leading or trailing spaces.");

       
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

     
        RuleFor(x => x)
            .Must(x => x.CurrentPassword != x.Password)
            .WithMessage("New password must be different from the current password.");
    }
}