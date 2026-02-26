using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DTO.ApplicationUserDto;
using InvoiceProject.Models;
using Microsoft.AspNetCore.Identity;

namespace InvoiceProject.Services;

public class ApplicationUsersService : IApplicationUsersService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUsersService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUserResponse> Login(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid name or password");
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!isValidPassword)
        {
            throw new UnauthorizedAccessException("Invalid name or password");
        }
        return new ApplicationUserResponse
        {
            Email = user.Email!
        };
    }

    public async Task<ApplicationUserResponse> Register(RegisterRequest registerRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException("User with this email already exists");
        }

        var user = new ApplicationUser
        {
            UserName = registerRequest.FirstName + registerRequest.LastName,
            Email = registerRequest.Email,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        var result = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed: {errors}");
        }
        return new ApplicationUserResponse
        {
            Email = user.Email
        };
    }



}