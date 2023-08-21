namespace Ecommerce.Catalog.Application.Notifications.Product.CreateProduct
{
    public class CreateProductNotification : IntegrationEvent
    {
        public Guid Id { get; set; }
    }
}