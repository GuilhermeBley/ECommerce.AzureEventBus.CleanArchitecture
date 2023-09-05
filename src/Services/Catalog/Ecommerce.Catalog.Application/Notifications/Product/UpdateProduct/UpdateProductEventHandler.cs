using Ecommerce.EventBus.Events;

namespace Ecommerce.Catalog.Application.Notifications.Product.UpdateProduct
{
    public class UpdateProductEventHandler : IIntegrationEventHandler<UpdateProductEvent>
    {
        public UpdateProductEventHandler()
        {
        }

        public async Task Handle(UpdateProductEvent notification)
            => await Task.CompletedTask;
    }
}
