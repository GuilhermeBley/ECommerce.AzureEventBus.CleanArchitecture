namespace Ecommerce.Catalog.Application.Notifications.Product.CreateProduct;

public class CreateProductEventHandler : IIntegrationEventHandler<CreateProductNotification>
{
    public CreateProductEventHandler()
    {
    }

    public async Task Handle(CreateProductNotification notification)
        => await Task.CompletedTask;
}
