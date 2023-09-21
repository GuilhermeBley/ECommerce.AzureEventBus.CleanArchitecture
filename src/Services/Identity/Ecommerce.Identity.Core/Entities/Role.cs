namespace Ecommerce.Identity.Core.Entities.Identity;

public class Role
{
    public const string ALLOWED_NAME_VALUE = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string NormalizedName { get; private set; }

    private Role(Guid id, string name, string normalizedName)
    {
        Id = id;
        Name = name;
        NormalizedName = normalizedName;
    }

    public override bool Equals(object? obj)
    {
        return obj is Role role &&
               Id.Equals(role.Id) &&
               Name == role.Name &&
               NormalizedName == role.NormalizedName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, NormalizedName);
    }

    public static Result<Role> Create(Guid id, string name)
    {
        ResultBuilder<Role> resultBuilder = new();

        resultBuilder.AddIf(
            string.IsNullOrWhiteSpace(name) ||
            name.Any(c => !ALLOWED_NAME_VALUE.Contains(c)),
            ErrorEnum.InvalidRoleName
        );

        if (resultBuilder.TryFailed(out Result<Role>? result))
            return result;

        var normalizedName = name.ToUpperInvariant();

        return resultBuilder.Success(
            new Role(id, name, normalizedName)
        );
    }
}
