namespace InvoiceProject.DTO.ApplicationUserDto;

public class RegisterRequest
{

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmedPassword { get; set; } = string.Empty;


}


public class LoginRequest
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}



public class ApplicationUserResponse
{
    public string Email { get; set; } = string.Empty;

}