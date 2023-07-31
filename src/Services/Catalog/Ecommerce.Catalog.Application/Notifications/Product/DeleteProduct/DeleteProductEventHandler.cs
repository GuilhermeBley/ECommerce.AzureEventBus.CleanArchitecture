namespace Ecommerce.Catalog.Application.Notifications.Product.DeleteProduct;

public class DeleteProductEventHandler : IAppNotificationHandler<DeleteProductNotification>
{
    private readonly IEventBus _eventBus;

    public DeleteProductEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteProductNotification notification, CancellationToken cancellationToken)
        =>  await _eventBus.PublishAsync(
            new DeleteProductNotificationIntegrationEvent
            {
                Id = notification.Id,
                Name = notification.Name,
                Value = notification.Value,
            });

    private class DeleteProductNotificationIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
    }
}
