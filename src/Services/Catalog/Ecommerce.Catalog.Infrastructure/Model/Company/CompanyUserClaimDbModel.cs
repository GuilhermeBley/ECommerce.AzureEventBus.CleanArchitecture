using Ecommerce.Catalog.Infrastructure.Model.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Catalog.Infrastructure.Model.Company;

public class CompanyUserClaimDbModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public UserDbModel User { get; set; } = null!;
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid CompanyId { get; set; }
    [Required]
    public CompanyDbModel Company { get; set; } = null!;
    [Required]
    [StringLength(250)]
    public string ClaimType { get; set; } = string.Empty;
    [Required]
    [StringLength(250)]
    public string ClaimValue { get; set; } = string.Empty;
}
