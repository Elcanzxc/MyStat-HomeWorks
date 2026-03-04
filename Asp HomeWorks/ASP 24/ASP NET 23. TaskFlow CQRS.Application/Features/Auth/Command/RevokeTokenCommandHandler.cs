using MediatR;
using ASP_NET_23._TaskFlow_CQRS.Application.Interfaces;

namespace ASP_NET_23._TaskFlow_CQRS.Application.Features.Auth.Command;



public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RevokeTokenCommandHandler(
        IJwtTokenService jwtTokenService,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _jwtTokenService = jwtTokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task Handle(RevokeTokenCommand request, CancellationToken ct)
    {
        string jti;
        try
        {
            (_, jti) = _jwtTokenService.ValidateRefreshTokenAndGetJti(request.RefreshToken, validateLifetime: false);
        }
        catch
        {
            return;
        }

     
        var storedToken = await _refreshTokenRepository.GetByJwtIdAsync(jti);

      
        if (storedToken != null && storedToken.IsActive)
        {
            storedToken.RevokedAt = DateTime.UtcNow;
            await _refreshTokenRepository.UpdateAsync(storedToken);
        }
    }
}