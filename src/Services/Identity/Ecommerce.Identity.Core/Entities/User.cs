namespace Ecommerce.Identity.Core.Entities;

public class User
{
    public const string ALLOWED_PASSWORD 
        = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 " + "!@#$%¨&*()_+-=";

    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public string? NickName { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public bool EmailConfirmed { get; private set; }

    /// <summary>
    /// Phone number
    /// </summary>
    public string? PhoneNumber { get; private set; }

    /// <summary>
    /// Is phone number confirmed
    /// </summary>
    public bool PhoneNumberConfirmed { get; private set; }

    /// <summary>
    /// Is two factory enabled
    /// </summary>
    public bool TwoFactoryEnabled { get; private set; }

    /// <summary>
    /// Date until lockout
    /// </summary>
    public DateTime? LockOutEnd { get; private set; }

    /// <summary>
    /// Is lockout enabled
    /// </summary>
    public bool LockOutEnabled { get; private set; }

    /// <summary>
    /// Count of fails access
    /// </summary>
    public int AccessFailedCount { get; private set; }

    private User(Guid id, string email, string name, string? nickName, string passwordHash, string passwordSalt, bool emailConfirmed, string? phoneNumber, bool phoneNumberConfirmed, bool twoFactoryEnabled, DateTime? lockOutEnd, bool lockOutEnabled, int accessFailedCount)
    {
        Id = id;
        Email = email;
        Name = name;
        NickName = nickName;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        EmailConfirmed = emailConfirmed;
        PhoneNumber = phoneNumber;
        PhoneNumberConfirmed = phoneNumberConfirmed;
        TwoFactoryEnabled = twoFactoryEnabled;
        LockOutEnd = lockOutEnd;
        LockOutEnabled = lockOutEnabled;
        AccessFailedCount = accessFailedCount;
    }

    public override bool Equals(object? obj)
    {
        return obj is User user &&
               Id.Equals(user.Id) &&
               Email == user.Email &&
               Name == user.Name &&
               NickName == user.NickName &&
               PasswordHash == user.PasswordHash &&
               PasswordSalt == user.PasswordSalt &&
               EmailConfirmed == user.EmailConfirmed &&
               PhoneNumber == user.PhoneNumber &&
               PhoneNumberConfirmed == user.PhoneNumberConfirmed &&
               TwoFactoryEnabled == user.TwoFactoryEnabled &&
               LockOutEnd == user.LockOutEnd &&
               LockOutEnabled == user.LockOutEnabled &&
               AccessFailedCount == user.AccessFailedCount;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(Id);
        hash.Add(Email);
        hash.Add(Name);
        hash.Add(NickName);
        hash.Add(PasswordHash);
        hash.Add(PasswordSalt);
        hash.Add(EmailConfirmed);
        hash.Add(PhoneNumber);
        hash.Add(PhoneNumberConfirmed);
        hash.Add(TwoFactoryEnabled);
        hash.Add(LockOutEnd);
        hash.Add(LockOutEnabled);
        hash.Add(AccessFailedCount);
        return hash.ToHashCode();
    }

    public static Result<User> Create(
        Guid id,
        string email,
        string name,
        string? nickName,
        string password,
        string? phoneNumber,
        bool twoFactoryEnabled = false,
        bool lockOutEnabled = true,
        bool phoneNumberConfirmed = false,
        bool emailConfirmed = false,
        DateTime? lockOutEnd = null,
        int accessFailedCount = 0)
    {
        var passwordResult = IsValidPassword(password);

        var hashedPassword = Security.HashConvert.CreateHashedPassword(password);

        var result = CreateWithHashedPassword(
            id: id,
            email: email,
            name: name,
            nickName: nickName,
            passwordHash: hashedPassword.HashBase64,
            passwordSalt: hashedPassword.Salt,
            phoneNumber: phoneNumber,
            twoFactoryEnabled: twoFactoryEnabled,
            lockOutEnabled: lockOutEnabled,
            phoneNumberConfirmed: phoneNumberConfirmed,
            emailConfirmed: emailConfirmed,
            lockOutEnd: lockOutEnd,
            accessFailedCount: accessFailedCount
        );

        if (result.IsFailure || passwordResult.IsFailure)
            return result.ConcatErrors(passwordResult);

        return result;
    }

    public static Result<User> CreateWithHashedPassword(
        Guid id,
        string email,
        string name,
        string? nickName,
        string passwordHash,
        string passwordSalt,
        string? phoneNumber,
        bool twoFactoryEnabled = false,
        bool lockOutEnabled = true,
        bool phoneNumberConfirmed = false,
        bool emailConfirmed = false,
        DateTime? lockOutEnd = null,
        int accessFailedCount = 0)
    {
        ResultBuilder<User> resultBuilder = new();

        resultBuilder.AddIfIsInRange(email, ErrorEnum.InvalidEmail, minValue: 3, maxValue: 255);

        resultBuilder.AddIfIsInRange(nickName, ErrorEnum.InvalidNickName, minValue: 3, maxValue: 255, checkNull: false);

        resultBuilder.AddIfIsInRange(name, ErrorEnum.InvalidName, minValue: 3, maxValue: 255);

        resultBuilder.AddIf(
            phoneNumber is null ?
                false
                : phoneNumber.All(char.IsNumber) && phoneNumber.Length > 5 && phoneNumber.Length < 15,
            ErrorEnum.InvalidPhoneNumber);

        if (resultBuilder.TryFailed(out Result<User>? result))
            return result;

        return resultBuilder.Success(
            new User(id: id, email: email, name: name, nickName: nickName,
            passwordHash: passwordHash ?? string.Empty, passwordSalt: passwordSalt ?? string.Empty,
            emailConfirmed: emailConfirmed, phoneNumber: phoneNumber,
            phoneNumberConfirmed: phoneNumberConfirmed, twoFactoryEnabled: twoFactoryEnabled,
            lockOutEnd: lockOutEnd, lockOutEnabled: lockOutEnabled, accessFailedCount: accessFailedCount)
        );
    }

    public static Result IsValidPassword(string password)
    {
        ResultBuilder resultBuilder = new();

        resultBuilder.AddIf(
            string.IsNullOrWhiteSpace(password) ||
            password.Any(c => !ALLOWED_PASSWORD.Contains(c)) ||
            password.Any(c => !char.IsLetter(c)) ||
            password.Any(c => !char.IsDigit(c)),
            ErrorEnum.InvalidPassword
        );

        return resultBuilder.GetResult();
    }
}
