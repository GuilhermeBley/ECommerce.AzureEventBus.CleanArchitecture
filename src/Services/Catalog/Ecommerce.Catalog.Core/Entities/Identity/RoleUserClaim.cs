namespace Ecommerce.Catalog.Core.Entities.Identity;

public class RoleUserClaim
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    private RoleUserClaim(Guid id, Guid userId, Guid roleId)
    {
        Id = id;
        UserId = userId;
        RoleId = roleId;
    }

    public override bool Equals(object? obj)
    {
        return obj is RoleUserClaim claim &&
               Id.Equals(claim.Id) &&
               UserId.Equals(claim.UserId) &&
               RoleId.Equals(claim.RoleId);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, UserId, RoleId);
    }

    public static Result<RoleUserClaim> Create(Guid id, User user, Role role)
        => Create(id, user.Id, role.Id);

    public static Result<RoleUserClaim> Create(Guid id, Guid userId, Guid roleId)
        => Result<RoleUserClaim>.Success(
            new RoleUserClaim(id, userId, roleId)
        );
}
