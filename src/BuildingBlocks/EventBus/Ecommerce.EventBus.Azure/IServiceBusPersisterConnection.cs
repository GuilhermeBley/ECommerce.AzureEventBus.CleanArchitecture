using Azure.Messaging.ServiceBus;

namespace Ecommerce.EventBus.Azure
{
    public interface IServiceBusPersisterConnection
    {
        string ConnectionString { get; }
        ServiceBusSender CreateModel(string queueOrTopicName);
    }
}
