using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO.ApplicationUserDto;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IApplicationUsersService _applicationUser;

    public UserController(IApplicationUsersService applicationUser)
    {
        _applicationUser = applicationUser;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> Register([FromBody] UserRegister registerRequest)
    {
        var result = await _applicationUser.Register(registerRequest);

        return Ok(ApiResponse<UserResponse>
            .SuccessResponse(result, "User registered succesfully"));
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> Login([FromBody] UserLogin loginRequest)
    {
        var result = await _applicationUser.Login(loginRequest);

        return Ok(ApiResponse<UserResponse>
            .SuccessResponse(result, "Login succesfully"));
    }

    [HttpPatch("profile")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> UpdateProfile([FromBody] UserUpdate userUpdate,string email)
    {
        var result = await _applicationUser.UpdateProfile(userUpdate, email);

        return Ok(ApiResponse<UserResponse>
            .SuccessResponse(result, "Profile updated succesfully"));
    }

    [HttpPatch("password")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> UpdatePassword([FromBody] UserPasswordUpdate userPasswordUpdate,string email)
    {
        var result = await _applicationUser.UpdatePassword(userPasswordUpdate,email);

        return Ok(ApiResponse<UserResponse>
            .SuccessResponse(result, "Password updated succesfully"));
    }
}
