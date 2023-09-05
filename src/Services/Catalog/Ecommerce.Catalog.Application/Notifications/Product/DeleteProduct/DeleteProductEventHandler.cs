using Ecommerce.EventBus.Events;

namespace Ecommerce.Catalog.Application.Notifications.Product.DeleteProduct;

public class DeleteProductEventHandler : IIntegrationEventHandler<DeleteProductEvent>
{
    public DeleteProductEventHandler()
    {
    }

    public async Task Handle(DeleteProductEvent notification)
        =>  await Task.CompletedTask;
}
