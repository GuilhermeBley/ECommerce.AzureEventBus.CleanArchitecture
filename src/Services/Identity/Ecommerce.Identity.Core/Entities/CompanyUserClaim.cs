namespace Ecommerce.Identity.Core.Entities;

public class CompanyUserClaim : ClaimEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CompanyId { get; private set; }

    private CompanyUserClaim(Guid id, Guid userId, Guid companyId, string claimType, string claimValue)
    {
        Id = id;
        UserId = userId;
        ClaimType = claimType;
        ClaimValue = claimValue;
        CompanyId = companyId;
    }

    public override bool Equals(object? obj)
    {
        return obj is CompanyUserClaim claim &&
               IdEntity.Equals(claim.IdEntity) &&
               ClaimType == claim.ClaimType &&
               ClaimValue == claim.ClaimValue &&
               Id.Equals(claim.Id) &&
               CompanyId.Equals(claim.CompanyId) &&
               UserId.Equals(claim.UserId);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdEntity, ClaimType, ClaimValue, Id, UserId, CompanyId);
    }

    public static Result<CompanyUserClaim> Create(Guid id, Guid userId, Guid companyId, string claimType, string claimValue)
    {
        var resultClaim = CheckClaim(claimType, claimValue);

        ResultBuilder<CompanyUserClaim> resultBuilder = new(resultClaim);

        if (resultBuilder.TryFailed(out Result<CompanyUserClaim>? result))
            return result;

        return resultBuilder.Success(
            new CompanyUserClaim(id, userId, companyId, claimType, claimValue)
        );
    }
}
