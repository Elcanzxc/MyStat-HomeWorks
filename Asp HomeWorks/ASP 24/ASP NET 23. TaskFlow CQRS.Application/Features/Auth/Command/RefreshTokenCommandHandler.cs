using ASP_NET_23._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_23._TaskFlow_CQRS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_23._TaskFlow_CQRS.Application.Features.Auth.Command;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthUserStore _authUserStore;
    private readonly ITokenGenerator _tokenGenerator; 

    public RefreshTokenCommandHandler(
        IJwtTokenService jwtTokenService,
        IRefreshTokenRepository refreshTokenRepository,
        IAuthUserStore authUserStore,
        ITokenGenerator tokenGenerator)
    {
        _jwtTokenService = jwtTokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _authUserStore = authUserStore;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
       
        var (userId, jti) = _jwtTokenService.ValidateRefreshTokenAndGetJti(request.RefreshToken);

        
        var storedToken = await _refreshTokenRepository.GetByJwtIdAsync(jti);
        if (storedToken is null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        if (!storedToken.IsActive)
            throw new UnauthorizedAccessException("Refresh token has been revoked or expired");

        
        storedToken.RevokedAt = DateTime.UtcNow;

        
        var email = await _authUserStore.GetEmailAsync(userId);
        var newTokens = await _tokenGenerator.GenerateAuthResponseAsync(userId, email);

      
        var newJti = _jwtTokenService.GetJtiFromRefreshToken(newTokens.RefreshToken);
        if (!string.IsNullOrEmpty(newJti))
        {
            var newStoredToken = await _refreshTokenRepository.GetByJwtIdAsync(newJti);
            if (newStoredToken is not null)
                storedToken.ReplacedByJwtId = newStoredToken.JwtId;
        }

        await _refreshTokenRepository.UpdateAsync(storedToken);

        return newTokens;
    }
}