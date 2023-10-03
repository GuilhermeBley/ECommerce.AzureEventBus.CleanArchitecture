namespace Ecommerce.Identity.Application.Commands.User.CreateUser;

public class CreateUserRequest
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NickName { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = string.Empty;
}
