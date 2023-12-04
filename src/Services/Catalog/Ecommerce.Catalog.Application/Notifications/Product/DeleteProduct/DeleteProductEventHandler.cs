using Ecommerce.EventBus.Events;

namespace Ecommerce.Catalog.Application.Notifications.Product.DeleteProduct;

public class DeleteProductEventHandler : IIntegrationEventHandler<DisableProductEvent>
{
    public DeleteProductEventHandler()
    {
    }

    public async Task Handle(DisableProductEvent notification)
        =>  await Task.CompletedTask;
}
