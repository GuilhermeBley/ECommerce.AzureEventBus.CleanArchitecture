namespace Ecommerce.Catalog.Core.Entities.Identity;

internal class RoleClaim : ClaimEntity
{
    public Guid IdClaim { get; private set; }
    public Guid IdRole { get; private set; }

    private RoleClaim(Guid idClaim, Guid idRole, string claimType, string claimValue)
    {
        IdClaim = idClaim;
        IdRole = idRole;
        ClaimType = claimType;
        ClaimValue = claimValue;
    }

    public override bool Equals(object? obj)
    {
        return obj is RoleClaim claim &&
               IdEntity.Equals(claim.IdEntity) &&
               ClaimType == claim.ClaimType &&
               ClaimValue == claim.ClaimValue &&
               IdClaim == claim.IdClaim &&
               IdRole == claim.IdRole;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdEntity, ClaimType, ClaimValue, IdClaim, IdRole);
    }

    public static Result<RoleClaim> Create(Guid idClaim, Guid idRole, string claimType, string claimValue)
    {
        var claimsResult = CheckClaim(claimType, claimValue);

        ResultBuilder<RoleClaim> resultBuilder = new(claimsResult);

        if (resultBuilder.TryFailed(out Result<RoleClaim>? result))
            return result;

        return resultBuilder.Success(
            new RoleClaim(idClaim, idRole, claimType, claimValue)    
        );
    }
}
