namespace Ecommerce.Catalog.Application.Notifications.Product.UpdateProduct
{
    public class UpdateProductNotification : IntegrationEvent
    {
        public Guid Id { get; set; }
    }
}