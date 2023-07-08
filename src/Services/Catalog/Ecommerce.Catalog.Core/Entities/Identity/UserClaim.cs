namespace Ecommerce.Catalog.Core.Entities.Identity;

public class UserClaim : ClaimEntity
{
    public Guid Id { get; set; }
    public Guid IdUser { get; set; }

    private UserClaim(Guid id, Guid idUser, string claimType, string claimValue)
    {
        Id = id;
        IdUser = idUser;
        ClaimType = claimType;
        ClaimValue = claimValue;
    }

    public override bool Equals(object? obj)
    {
        return obj is UserClaim claim &&
               IdEntity.Equals(claim.IdEntity) &&
               ClaimType == claim.ClaimType &&
               ClaimValue == claim.ClaimValue &&
               Id.Equals(claim.Id) &&
               IdUser.Equals(claim.IdUser);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdEntity, ClaimType, ClaimValue, Id, IdUser);
    }

    public static Result<UserClaim> Create(Guid id, Guid idUser, string claimType, string claimValue)
    {
        var resultClaim = CheckClaim(claimType, claimValue);

        ResultBuilder<UserClaim> resultBuilder = new(resultClaim);

        if (resultBuilder.TryFailed(out Result<UserClaim>? result))
            return result;

        return resultBuilder.Success(
            new UserClaim(id, idUser, claimType, claimValue)
        );
    }
}
