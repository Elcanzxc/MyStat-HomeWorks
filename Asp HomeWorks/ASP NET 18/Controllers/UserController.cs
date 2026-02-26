using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO.ApplicationUserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InvoiceProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUsersService _applicationUser;

    public UserController(IUsersService applicationUser)
    {
        _applicationUser = applicationUser;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> Register([FromBody] UserRegister registerRequest)
    {
        var result = await _applicationUser.Register(registerRequest);
        return Ok(ApiResponse<UserResponse>.SuccessResponse(result, "User registered successfully"));
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> Login([FromBody] UserLogin loginRequest)
    {
        var result = await _applicationUser.Login(loginRequest);
        return Ok(ApiResponse<UserResponse>.SuccessResponse(result, "Login successfully"));
    }


    [HttpPost("refresh")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> Refresh([FromBody] RefreshTokenRequest request)
    {
        var result = await _applicationUser.RefreshToken(request);
        return Ok(ApiResponse<UserResponse>.SuccessResponse(result, "Token refreshed successfully"));
    }

    [Authorize] 
    [HttpPatch("profile")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> UpdateProfile([FromBody] UserUpdate userUpdate)
    {
        // Извлекаем email из токена текущего пользователя
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email)) return Unauthorized();

        var result = await _applicationUser.UpdateProfile(userUpdate, email);
        return Ok(ApiResponse<UserResponse>.SuccessResponse(result, "Profile updated successfully"));
    }

    [Authorize] 
    [HttpPatch("password")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> UpdatePassword([FromBody] UserPasswordUpdate userPasswordUpdate)
    {
        // Извлекаем email из токена текущего пользователя
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email)) return Unauthorized();

        var result = await _applicationUser.UpdatePassword(userPasswordUpdate, email);
        return Ok(ApiResponse<UserResponse>.SuccessResponse(result, "Password updated successfully"));
    }
}