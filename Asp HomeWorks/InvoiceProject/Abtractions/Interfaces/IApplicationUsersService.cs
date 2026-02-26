using InvoiceProject.DTO.ApplicationUserDto;

namespace InvoiceProject.Abtractions.Interfaces;

public interface IApplicationUsersService
{
    Task<UserResponse> Register(UserRegister registerRequest);
    Task<UserResponse> Login(UserLogin loginRequest);

    Task<UserResponse> UpdateProfile(UserUpdate loginRequest,string email);

    Task<UserResponse> UpdatePassword(UserPasswordUpdate loginRequest, string email);

}
