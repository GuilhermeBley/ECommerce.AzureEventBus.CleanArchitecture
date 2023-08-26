using Ecommerce.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ecommerce.EventBus.Azure.Extensions.Di;

public static class EventBusServiceBusExtension
{
    public static IServiceCollection AddEventBus(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IEventBus>(
            provider => {

                var eventBusOptions = provider.GetRequiredService<IOptions<AzureServiceBusOptions>>();
                var logFactory = provider.GetRequiredService<ILoggerFactory>();

                var serviceBusPersisterConnection
                    = new DefaultServiceBusPersisterConnection(
                        logger: logFactory.CreateLogger<DefaultServiceBusPersisterConnection>(),
                        options: eventBusOptions
                    );

                var subsManager = new InMemoryEventBusSubscriptionsManager();

                return new EventBusServiceBus(
                    serviceBusPersisterConnection: serviceBusPersisterConnection,
                    logger: logFactory.CreateLogger<EventBusServiceBus>(),
                    subsManager: subsManager,
                    options: eventBusOptions,
                    provider: provider.CreateScope().ServiceProvider);
            });

        return serviceCollection;
    }
}
