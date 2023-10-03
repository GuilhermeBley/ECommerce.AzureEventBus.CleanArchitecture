namespace Ecommerce.Identity.Application.Commands.User.CreateUser;

public class CreateUserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NickName { get; set; }
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Is phone number confirmed
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// Is two factory enabled
    /// </summary>
    public bool TwoFactoryEnabled { get; set; }

    /// <summary>
    /// Date until lockout
    /// </summary>
    public DateTime? LockOutEnd { get; set; }

    /// <summary>
    /// Is lockout enabled
    /// </summary>
    public bool LockOutEnabled { get; set; }

    /// <summary>
    /// Count of fails access
    /// </summary>
    public int AccessFailedCount { get; set; }
}
