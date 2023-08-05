using Ecommerce.Catalog.Infrastructure.Model.Product;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Catalog.Infrastructure.Model.Company;

public class CompanyProductDbModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid CompanyId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public ProductDbModel Product { get; set; } = null!;
    [Required]
    public DateTime CreateAt { get; set; }
}
