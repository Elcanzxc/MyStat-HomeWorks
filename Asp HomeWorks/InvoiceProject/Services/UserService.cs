using AutoMapper;
using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.DataAccess;
using InvoiceProject.DTO.ApplicationUserDto;
using InvoiceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;

namespace InvoiceProject.Services;

public class UserService : IApplicationUsersService
{
    private readonly UserManager<User> _userManager;

    private readonly IMapper _mapper;


    public UserService(
            UserManager<User> userManager,
            IConfiguration configuration,
            InvoiceDbContext context,
            IMapper mapper) 
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserResponse> Login(UserLogin loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!isValidPassword)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

      
        return _mapper.Map<UserResponse>(user);
    }
    public async Task<UserResponse> Register(UserRegister registerRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException("User with this email already exists");
        }


        var user = _mapper.Map<User>(registerRequest);

        var result = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed: {errors}");
        }

        return _mapper.Map<UserResponse>(user);
    }
    public async Task<UserResponse> UpdateProfile(UserUpdate updateRequest,string email)
    {
      
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null) throw new KeyNotFoundException("User not found");

     
        _mapper.Map(updateRequest, user);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) throw new Exception("Update failed");

        return _mapper.Map<UserResponse>(user);
    }
    public async Task<UserResponse> UpdatePassword(UserPasswordUpdate passwordRequest,string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new KeyNotFoundException("User not found");

        var result = await _userManager.ChangePasswordAsync(user,
            passwordRequest.CurrentPassword,
            passwordRequest.Password);

        if (!result.Succeeded) throw new Exception("Password change failed");

        return _mapper.Map<UserResponse>(user);
    }


}
