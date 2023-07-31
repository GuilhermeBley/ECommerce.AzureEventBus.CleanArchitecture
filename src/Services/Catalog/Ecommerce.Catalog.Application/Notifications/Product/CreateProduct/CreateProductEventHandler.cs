namespace Ecommerce.Catalog.Application.Notifications.Product.CreateProduct;

public class CreateProductEventHandler : IAppNotificationHandler<CreateProductNotification>
{
    private readonly IEventBus _eventBus;

    public CreateProductEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(CreateProductNotification notification, CancellationToken cancellationToken)
        => await _eventBus.PublishAsync(
            new CreateProductNotificationIntegrationEvent
            {
                Id = notification.Id,
            });

    private class CreateProductNotificationIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
    }
}
