using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Catalog.Infrastructure.Model.Identity;

public class UserDbModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [StringLength(250)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [StringLength(250)]
    public string Name { get; set; } = string.Empty;
    public string? NickName { get; set; }
    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; } = string.Empty;
    [Required]
    [StringLength(255)]
    public string PasswordSalt { get; set; } = string.Empty;
    [Required]
    public bool EmailConfirmed { get; set; }
}
