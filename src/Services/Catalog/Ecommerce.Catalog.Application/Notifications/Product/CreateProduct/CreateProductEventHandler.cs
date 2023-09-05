using Ecommerce.EventBus.Events;

namespace Ecommerce.Catalog.Application.Notifications.Product.CreateProduct;

public class CreateProductEventHandler : IIntegrationEventHandler<CreateProductEvent>
{
    public CreateProductEventHandler()
    {
    }

    public async Task Handle(CreateProductEvent notification)
        => await Task.CompletedTask;
}
