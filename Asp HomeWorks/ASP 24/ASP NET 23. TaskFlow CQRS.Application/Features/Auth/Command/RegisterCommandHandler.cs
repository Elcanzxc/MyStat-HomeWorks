using ASP_NET_23._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_23._TaskFlow_CQRS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_23._TaskFlow_CQRS.Application.Features.Auth.Command;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IAuthUserStore _authUserStore;
    private readonly ITokenGenerator _tokenGenerator;

    public RegisterCommandHandler(IAuthUserStore authUserStore, ITokenGenerator tokenGenerator)
    {
        _authUserStore = authUserStore;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken ct)
    {

        var existingUserId = await _authUserStore.FindUserIdByEmailOrIdAsync(request.Email);
        if (existingUserId is not null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

 
        var registerDto = new RegisterRequestDto
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            ConfirmPassword = request.ConfirmPassword
        };

        var userId = await _authUserStore.CreateUserAsync(registerDto);

     
        await _authUserStore.AddToRoleAsync(userId, "User");

        return await _tokenGenerator.GenerateAuthResponseAsync(userId, request.Email);
    }
}