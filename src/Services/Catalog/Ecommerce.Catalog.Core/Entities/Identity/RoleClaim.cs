namespace Ecommerce.Catalog.Core.Entities.Identity;

internal class RoleClaim : ClaimEntity
{
    public int IdClaim { get; private set; }
    public int IdRole { get; private set; }

    private RoleClaim(int idClaim, int idRole, string claimType, string claimValue)
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

    public static Result<RoleClaim> Create(int idClaim, int idRole, string claimType, string claimValue)
    {
        var claimsResult = CheckClaim(claimType, claimValue);

        ResultBuilder<RoleClaim> resultBuilder = new(claimsResult);

        resultBuilder.AddIf(
            idClaim < 0, ErrorEnum.InvalidIdClaim
        );

        resultBuilder.AddIf(
            idRole < 0, ErrorEnum.InvalidIdRole
        );

        if (resultBuilder.TryFailed(out Result<RoleClaim>? result))
            return result;

        return resultBuilder.Success(
            new RoleClaim(idClaim, idRole, claimType, claimValue)    
        );
    }
}
