namespace Ecommerce.Catalog.Application.Notifications.Product.UpdateProduct
{
    internal class UpdateProductEventHandler : IAppNotificationHandler<UpdateProductNotification>
    {
        private readonly IEventBus _eventBus;

        public UpdateProductEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(UpdateProductNotification notification, CancellationToken cancellationToken)
            => await _eventBus.PublishAsync(
                new UpdateProductNotificationIntegrationEvent
                {
                    Id = notification.Id,
                });

        private class UpdateProductNotificationIntegrationEvent : IntegrationEvent
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public double Value { get; set; }
        }
    }
}
