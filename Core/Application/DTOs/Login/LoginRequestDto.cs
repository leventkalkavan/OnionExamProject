namespace Application.DTOs.Login;

public class LoginRequestDto
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
}