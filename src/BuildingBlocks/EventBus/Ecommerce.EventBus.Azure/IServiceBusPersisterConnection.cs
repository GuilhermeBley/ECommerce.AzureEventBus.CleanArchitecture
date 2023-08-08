using Azure.Messaging.ServiceBus;

namespace Ecommerce.EventBus.Azure
{
    public interface IServiceBusPersisterConnection
    {
        ServiceBusSender CreateModel(string queueOrTopicName);
    }
}
