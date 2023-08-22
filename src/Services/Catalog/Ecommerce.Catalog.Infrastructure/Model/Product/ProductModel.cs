using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Catalog.Infrastructure.Model.Product;

public class ProductDbModel
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public double Value { get; set; }
    public DateTime? UpdateAt { get; set; }
    [Required]
    public DateTime InsertAt { get; set; }
}
