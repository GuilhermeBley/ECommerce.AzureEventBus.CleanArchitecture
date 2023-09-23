namespace Ecommerce.Identity.Core.Entities;

public class UserClaim : ClaimEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    private UserClaim(Guid id, Guid userId, string claimType, string claimValue)
    {
        Id = id;
        UserId = userId;
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
               UserId.Equals(claim.UserId);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdEntity, ClaimType, ClaimValue, Id, UserId);
    }

    public static Result<UserClaim> Create(Guid id, Guid userId, string claimType, string claimValue)
    {
        var resultClaim = CheckClaim(claimType, claimValue);

        ResultBuilder<UserClaim> resultBuilder = new(resultClaim);

        if (resultBuilder.TryFailed(out Result<UserClaim>? result))
            return result;

        return resultBuilder.Success(
            new UserClaim(id, userId, claimType, claimValue)
        );
    }
}
