namespace Ecommerce.Identity.Core.Entities;

internal class RoleClaim : ClaimEntity
{
    public Guid ClaimId { get; private set; }
    public Guid RoleId { get; private set; }

    private RoleClaim(Guid claimId, Guid roleId, string claimType, string claimValue)
    {
        ClaimId = claimId;
        RoleId = roleId;
        ClaimType = claimType;
        ClaimValue = claimValue;
    }

    public override bool Equals(object? obj)
    {
        return obj is RoleClaim claim &&
               IdEntity.Equals(claim.IdEntity) &&
               ClaimType == claim.ClaimType &&
               ClaimValue == claim.ClaimValue &&
               ClaimId == claim.ClaimId &&
               RoleId == claim.RoleId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdEntity, ClaimType, ClaimValue, ClaimId, RoleId);
    }

    public static Result<RoleClaim> Create(Guid claimId, Guid roleId, string claimType, string claimValue)
    {
        var claimsResult = CheckClaim(claimType, claimValue);

        ResultBuilder<RoleClaim> resultBuilder = new(claimsResult);

        if (resultBuilder.TryFailed(out Result<RoleClaim>? result))
            return result;

        return resultBuilder.Success(
            new RoleClaim(claimId, roleId, claimType, claimValue)    
        );
    }
}
