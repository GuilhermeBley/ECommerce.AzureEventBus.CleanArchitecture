namespace Ecommerce.Catalog.Application.Notifications.Product.DeleteProduct;

public class DeleteProductEventHandler : IAppNotificationHandler<DeleteProductNotification>
{
    public Task Handle(DeleteProductNotification notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
