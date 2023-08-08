using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Ecommerce.EventBus.Azure;

public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection
{
    private readonly ILogger<DefaultServiceBusPersisterConnection> _logger;
    private readonly ConcurrentDictionary<string, ServiceBusSender>  _senders = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly ServiceBusClient _busClient;

    bool _disposed;

    public DefaultServiceBusPersisterConnection(
        string connectionString,
        ILogger<DefaultServiceBusPersisterConnection> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _busClient = new ServiceBusClient(connectionString);
    }

    public ServiceBusSender CreateModel(string queueOrTopicName)
    {
        if (_senders.TryGetValue(queueOrTopicName, out ServiceBusSender? sender))
            return sender;
        
        try
        {
            _semaphore.Wait();

            if (_senders.TryGetValue(queueOrTopicName, out sender))
                return sender;

            sender = _busClient.CreateSender(queueOrTopicName);

            _senders.TryAdd(queueOrTopicName, sender);
            return sender;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;
    }
}
