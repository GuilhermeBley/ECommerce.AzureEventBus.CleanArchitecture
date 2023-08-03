namespace Ecommerce.Catalog.Infrastructure.Model.Identity;

public class RoleClaimDbModel
{
    public Guid IdClaim { get; set; }
    public Guid IdRole { get; set; }
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
