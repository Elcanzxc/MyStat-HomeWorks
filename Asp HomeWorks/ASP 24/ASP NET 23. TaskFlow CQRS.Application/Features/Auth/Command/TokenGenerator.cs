using ASP_NET_23._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_23._TaskFlow_CQRS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_23._TaskFlow_CQRS.Application.Features.Auth.Command;

public interface ITokenGenerator
{
    Task<AuthResponseDto> GenerateAuthResponseAsync(string userId, string? email);
}

public class TokenGenerator : ITokenGenerator
{
    private readonly IAuthUserStore _authUserStore;
    private readonly IJwtTokenService _jwtTokenService;

    public TokenGenerator(IAuthUserStore authUserStore, IJwtTokenService jwtTokenService)
    {
        _authUserStore = authUserStore;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto> GenerateAuthResponseAsync(string userId, string? email)
    {
        var roles = await _authUserStore.GetRolesAsync(userId);
        var (accessToken, expiresAt) = await _jwtTokenService.GenerateAccessTokenAsync(userId, email ?? "", roles);
        var (refreshEntity, refreshJwt) = await _jwtTokenService.CreateRefreshTokenAsync(userId);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAt,
            RefreshToken = refreshJwt,
            RefreshTokenExpiresAt = refreshEntity.ExpiresAt,
            Email = email ?? "",
            Roles = roles
        };
    }
}
