using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Ecommerce.EventBus.Azure;

public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection
{
    private readonly ILogger<DefaultServiceBusPersisterConnection> _logger;
    private readonly IOptions<AzureServiceBusOptions> _options;
    private readonly ConcurrentDictionary<string, ServiceBusSender>  _senders = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly ServiceBusClient _busClient;

    bool _disposed;

    public DefaultServiceBusPersisterConnection(
        ILogger<DefaultServiceBusPersisterConnection> logger,
        IOptions<AzureServiceBusOptions> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options;
        _busClient = new ServiceBusClient(_options.Value.ConnectionString);
    }

    public ServiceBusSender CreateModel()
    {
        if (_senders.TryGetValue(_options.Value.TopicName, out ServiceBusSender? sender))
            return sender;
        
        try
        {
            _semaphore.Wait();

            if (_senders.TryGetValue(_options.Value.TopicName, out sender))
                return sender;

            sender = _busClient.CreateSender(_options.Value.TopicName);

            _senders.TryAdd(_options.Value.TopicName, sender);
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
