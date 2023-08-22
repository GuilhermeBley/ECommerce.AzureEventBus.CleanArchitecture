using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Catalog.Api.Model.Product;

public class PostProductModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;
    [Range(0, double.MaxValue)]
    public double Value { get; set; }
}
