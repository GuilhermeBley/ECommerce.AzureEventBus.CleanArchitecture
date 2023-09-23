namespace Ecommerce.Identity.Application.Model;

public class UserClaimModel
{
    public Guid IdClaim { get; set; }
    public Guid UserId { get; set; }
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
