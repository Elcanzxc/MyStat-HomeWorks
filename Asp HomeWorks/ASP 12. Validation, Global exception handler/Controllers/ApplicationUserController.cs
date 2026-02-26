using InvoiceProject.Abtractions.Interfaces;
using InvoiceProject.Common;
using InvoiceProject.DTO.ApplicationUserDto;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationUserController : ControllerBase
{
    private readonly IApplicationUsersService _applicationUser;

    public ApplicationUserController(IApplicationUsersService applicationUser)
    {
        _applicationUser = applicationUser;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApplicationUserResponse>> Register([FromBody] RegisterRequest registerRequest)
    {
        var result = await _applicationUser.Register(registerRequest);

        return Ok(ApiResponse<ApplicationUserResponse>
            .SuccessResponse(result, "User registered succesfully"));
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApplicationUserResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _applicationUser.Login(loginRequest);

        return Ok(ApiResponse<ApplicationUserResponse>
            .SuccessResponse(result, "Login succesfully"));
    }
}
