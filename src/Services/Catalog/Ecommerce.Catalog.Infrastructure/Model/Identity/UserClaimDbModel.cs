using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Catalog.Infrastructure.Model.Identity;

public class UserClaimDbModel
{
    [Key]
    public Guid IdClaim { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public UserDbModel User { get; set; } = null!;
    [Required]
    [StringLength(250)]
    public string ClaimType { get; set; } = string.Empty;
    [Required]
    [StringLength(250)]
    public string ClaimValue { get; set; } = string.Empty;
}
