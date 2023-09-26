namespace Ecommerce.Identity.Application.Commands.Authentication.LoginUser;

public class LoginUserResponse
{
    public string Token { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
