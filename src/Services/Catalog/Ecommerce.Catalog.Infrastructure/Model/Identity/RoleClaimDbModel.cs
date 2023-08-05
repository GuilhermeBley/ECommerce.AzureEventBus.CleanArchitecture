using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Catalog.Infrastructure.Model.Identity;

public class RoleClaimDbModel
{
    [Key]
    public Guid IdClaim { get; set; }
    [Required]
    public Guid IdRole { get; set; }
    public RoleDbModel Role { get; set; } = null!;
    [Required]
    [StringLength(250)]
    public string ClaimType { get; set; } = string.Empty;
    [Required]
    [StringLength(250)]
    public string ClaimValue { get; set; } = string.Empty;
}
