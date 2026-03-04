using ASP_NET_23._TaskFlow_CQRS.Application.Common;
using ASP_NET_23._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_23._TaskFlow_CQRS.Application.Features.Auth.Command;
using ASP_NET_23._TaskFlow_CQRS.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_23._TaskFlow_CQRS.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "User registered successfully"));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Login successfully"));
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Token refresh successfully"));
    }

    [HttpPost("revoke")]
    public async Task<ActionResult> Revoke([FromBody] RevokeTokenCommand command)
    {
        await _mediator.Send(command);
        return Ok(ApiResponse<object>.SuccessResponse("Token revoke successfully"));
    }
}