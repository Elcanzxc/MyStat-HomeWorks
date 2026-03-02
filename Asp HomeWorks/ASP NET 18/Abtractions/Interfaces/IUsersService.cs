using InvoiceProject.DTO;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IUsersService
{
    Task<UserResponse> Login(UserLogin loginRequest);

    Task<UserResponse> RefreshToken(RefreshTokenRequest request);

    Task<UserResponse> Register(UserRegister registerRequest);

    Task<UserResponse> UpdateProfile(UserUpdate updateRequest, string email);

    Task<UserResponse> UpdatePassword(UserPasswordUpdate passwordRequest, string email);

}
