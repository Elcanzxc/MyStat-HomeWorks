using ASP_NET_23._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_23._TaskFlow_CQRS.Application.Interfaces;
using MediatR;


namespace ASP_NET_23._TaskFlow_CQRS.Application.Features.Auth.Command;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IAuthUserStore _authUserStore;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginCommandHandler(IAuthUserStore authUserStore, ITokenGenerator tokenGenerator)
    {
        _authUserStore = authUserStore;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken ct)
    {
        var userId = await _authUserStore.FindUserIdByEmailOrIdAsync(request.Email);

        if (userId is null || !await _authUserStore.CheckPasswordAsync(userId, request.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return await _tokenGenerator.GenerateAuthResponseAsync(userId, request.Email);
    }
}