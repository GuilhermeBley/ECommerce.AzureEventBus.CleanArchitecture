namespace Ecommerce.Catalog.Application.Notifications.Product.DeleteProduct;

public class DeleteProductNotification
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
}
