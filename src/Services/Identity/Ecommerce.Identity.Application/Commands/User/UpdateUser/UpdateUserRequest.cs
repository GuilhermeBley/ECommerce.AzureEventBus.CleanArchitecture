namespace Ecommerce.Identity.Application.Commands.User.UpdateUser;

public class UpdateUserRequest
{
    public Guid IdToUpdate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NickName { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    public string? PhoneNumber { get; set; }
}
