using Ecommerce.EventBus.Events;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Catalog.Application.Notifications.Product.UpdateProduct
{
    public class UpdateProductEventHandler : IIntegrationEventHandler<UpdateProductEvent>
    {
        private readonly ILogger<UpdateProductEventHandler> _logger;

        public UpdateProductEventHandler(ILogger<UpdateProductEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(UpdateProductEvent notification)
        {
            _logger.LogInformation("Product '{0}' updated.", notification.Id);
            await Task.CompletedTask;
        }
    }
}
