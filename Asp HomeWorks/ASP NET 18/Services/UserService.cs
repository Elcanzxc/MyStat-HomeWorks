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
using System.Security.Cryptography;
using System.Text;

namespace InvoiceProject.Services;

public class UserService : IUsersService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(
            UserManager<User> userManager,
            IConfiguration configuration,
            IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration; 
    }

    public async Task<UserResponse> Login(UserLogin loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

       
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

       
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        var response = _mapper.Map<UserResponse>(user);

  
        response.AccessToken = accessToken;
        response.RefreshToken = refreshToken;

        return response;
    }
    public async Task<UserResponse> RefreshToken(RefreshTokenRequest request)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);

        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        var newAccessToken = GenerateAccessToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

       
        var response = _mapper.Map<UserResponse>(user);
        response.AccessToken = newAccessToken;
        response.RefreshToken = newRefreshToken;

        return response;
    }
    private string GenerateAccessToken(User user)
    {
        var jwtOptions = _configuration.GetSection("JwtOptions");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions["Key"]!));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("FirstName", user.FirstName)
        };

        var token = new JwtSecurityToken(
            issuer: jwtOptions["Issuer"],
            audience: jwtOptions["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtOptions["ExpirationTime"])),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    public async Task<UserResponse> Register(UserRegister registerRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (existingUser is not null) throw new InvalidOperationException("User already exists");

        var user = _mapper.Map<User>(registerRequest);
        var result = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded) throw new Exception("User creation failed");

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
