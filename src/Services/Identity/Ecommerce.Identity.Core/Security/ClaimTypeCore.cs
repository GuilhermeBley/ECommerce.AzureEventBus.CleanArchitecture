using System.Security.Claims;

namespace Ecommerce.Identity.Core.Security;

public static class ClaimTypeCore
{
    public const string DEFAULT_ROLE = ClaimTypes.Role;
    public const string DEFAULT_NAME = ClaimTypes.Name;
    public const string DEFAULT_EMAIL = ClaimTypes.Email;
    public const string DEFAULT_ID = ClaimTypes.NameIdentifier;
    public const string DEFAULT_COMPANY_ID = "companyid";

    public static Claim CreateClaimId(Guid id)
        => CreateClaimId(id.ToString());

    public static Claim CreateClaimId(string id)
        => new Claim(DEFAULT_ID, id);

    public static Claim CreateClaimEmail(string email)
        => new Claim(DEFAULT_EMAIL, email);

    public static Claim CreateName(string name)
        => new Claim(DEFAULT_NAME, name);

    public static Claim CreateClaimRole(string roleValue)
        => new Claim(DEFAULT_ROLE, roleValue);
}
