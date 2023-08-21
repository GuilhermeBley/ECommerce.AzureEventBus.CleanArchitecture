namespace Ecommerce.Catalog.Application.Notifications.Product.UpdateProduct
{
    public class UpdateProductEventHandler : IIntegrationEventHandler<UpdateProductNotification>
    {
        public UpdateProductEventHandler()
        {
        }

        public async Task Handle(UpdateProductNotification notification)
            => await Task.CompletedTask;
    }
}
