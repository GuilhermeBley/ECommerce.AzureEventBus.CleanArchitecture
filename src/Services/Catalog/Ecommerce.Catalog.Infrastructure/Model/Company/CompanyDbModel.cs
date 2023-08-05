using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Catalog.Infrastructure.Model.Company;

public class CompanyDbModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public DateTime? UpdateAt { get; set; }
    [Required]
    public DateTime CreateAt { get; set; }
}
