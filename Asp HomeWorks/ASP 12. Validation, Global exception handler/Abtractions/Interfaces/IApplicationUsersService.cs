using InvoiceProject.DTO.ApplicationUserDto;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IApplicationUsersService
{
    Task<ApplicationUserResponse> Register(RegisterRequest registerRequest);
    Task<ApplicationUserResponse> Login(LoginRequest loginRequest);
}
