namespace Ecommerce.Catalog.Infrastructure.Model.Identity;

public class RoleUserClaimDbModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
