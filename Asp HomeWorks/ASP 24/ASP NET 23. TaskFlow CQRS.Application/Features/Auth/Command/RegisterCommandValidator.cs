using ASP_NET_23._TaskFlow_CQRS.Application.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_23._TaskFlow_CQRS.Application.Features.Auth.Command;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required")
            .MinimumLength(2).WithMessage("Firstname must be at least 2 characters long");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required")
            .MinimumLength(2).WithMessage("Lastname must be at least 2 characters long");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Password();

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirmed password is required")
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}
