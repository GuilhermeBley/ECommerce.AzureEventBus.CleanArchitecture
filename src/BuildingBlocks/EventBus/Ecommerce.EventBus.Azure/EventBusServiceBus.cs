using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.EventBus.Azure
{
    internal class EventBusServiceBus
    {
        public EventBusServiceBus(IServiceBusPersisterConnection serviceBusPersisterConnection,
            ILogger<EventBusServiceBus> logger, IEventBusSubscriptionsManager subsManager, string subscriptionClientName,
            ILifetimeScope autofac)
        {
            _serviceBusPersisterConnection = serviceBusPersisterConnection;
            _logger = logger;
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();

            _subscriptionClient = new SubscriptionClient(serviceBusPersisterConnection.ServiceBusConnectionStringBuilder,
                subscriptionClientName);
            _autofac = autofac;

            _subscriptionClient.PrefetchCount = 300;

            //var check = _subscriptionClient.PrefetchCount;

            //RemoveDefaultRule();
            RegisterSubscriptionClientMessageHandler();
        }
    }
}
