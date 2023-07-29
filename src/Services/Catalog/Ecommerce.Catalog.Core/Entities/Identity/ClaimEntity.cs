namespace Ecommerce.Catalog.Core.Entities.Identity;

public class ClaimEntity : Entity
{
    public const string ALLOWED_TYPE_VALUE = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
    public const int MAX_CHAR_TYPE_VALUE = 250;
    public const int MIN_CHAR_TYPE_VALUE = 2;

    public string ClaimType { get; internal protected set; } = string.Empty;
    public string ClaimValue { get; internal protected set; } = string.Empty;

    public static Result CheckClaim(string? type, string? value)
    {
        ResultBuilder resultBuilder = new();

        var isInvalidType = string.IsNullOrEmpty(type) ||
            type.Length > MAX_CHAR_TYPE_VALUE ||
            type.Length < MIN_CHAR_TYPE_VALUE ||
            type.Any(c => !ALLOWED_TYPE_VALUE.Contains(c));

        resultBuilder.AddIf(isInvalidType, ErrorEnum.InvalidClaimType);

        bool isInvalidValue = string.IsNullOrEmpty(value) ||
            value.Length > MAX_CHAR_TYPE_VALUE ||
            value.Length < MIN_CHAR_TYPE_VALUE ||
            value.Any(c => !ALLOWED_TYPE_VALUE.Contains(c));

        resultBuilder.AddIf(isInvalidValue, ErrorEnum.InvalidClaimValue);

        return resultBuilder.GetResult();
    }
}
