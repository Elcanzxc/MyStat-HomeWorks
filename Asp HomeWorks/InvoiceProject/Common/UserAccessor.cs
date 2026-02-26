using InvoiceProject.Abtractions.Interfaces;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace InvoiceProject.Common;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _accessor;
    public UserAccessor(IHttpContextAccessor accessor) => _accessor = accessor;

    public string UserId => _accessor.HttpContext?.User.FindFirstValue(OpenIddictConstants.Claims.Subject)
                           ?? throw new UnauthorizedAccessException("User is not authenticated");
}