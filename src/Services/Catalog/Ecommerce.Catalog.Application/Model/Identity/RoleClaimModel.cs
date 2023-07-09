namespace Ecommerce.Catalog.Application.Model.Identity;

public class RoleClaimModel
{
    public Guid IdClaim { get; set; }
    public Guid IdRole { get; set; }
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
