namespace Ecommerce.Catalog.Application.Notifications.Product.DeleteProduct;

public class DeleteProductEventHandler : IIntegrationEventHandler<DeleteProductNotification>
{
    public DeleteProductEventHandler()
    {
    }

    public async Task Handle(DeleteProductNotification notification)
        =>  await Task.CompletedTask;
}
