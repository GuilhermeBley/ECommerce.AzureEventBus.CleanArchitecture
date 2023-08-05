using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Catalog.Infrastructure.Model.Identity;

public class RoleUserClaimDbModel
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public UserDbModel User { get; set; } = null!;
    [Required]
    public Guid RoleId { get; set; }
    [Required]
    public RoleDbModel Role { get; set; } = null!;
}
