namespace Ecommerce.Catalog.Application.Commands.Product.CreateProduct;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Value { get; set; }
}
