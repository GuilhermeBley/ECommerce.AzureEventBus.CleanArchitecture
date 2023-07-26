namespace Ecommerce.Catalog.Application.Commands.Product.UpdateProduct;

public class UpdateProductRequest
{
    public Guid ProductId { get; set; }
    public string NewName { get; set; } = string.Empty;
    public string NewDescription { get; set; } = string.Empty;
    public double NewValue { get; set; }
}
