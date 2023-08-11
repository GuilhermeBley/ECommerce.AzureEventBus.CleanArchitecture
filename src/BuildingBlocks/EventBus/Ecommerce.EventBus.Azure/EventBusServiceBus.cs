using Azure.Messaging.ServiceBus;
using Ecommerce.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.EventBus.Azure
{
    internal class EventBusServiceBus
    {
        private const string INTEGRATION_EVENT_SUFIX = nameof(IntegrationEvent);

        private readonly IServiceBusPersisterConnection _serviceBusPersisterConnection;
        private readonly ILogger<EventBusServiceBus> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ServiceBusClient _subscriptionClient;
        private readonly IServiceScope _scopeLifeTime;

        public EventBusServiceBus(
            IServiceBusPersisterConnection serviceBusPersisterConnection,
            ILogger<EventBusServiceBus> logger, 
            IEventBusSubscriptionsManager subsManager, 
            string subscriptionClientName,
            IServiceProvider provider)
        {
            _serviceBusPersisterConnection = serviceBusPersisterConnection;
            _logger = logger;
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();

            _subscriptionClient = new ServiceBusClient(serviceBusPersisterConnection.ConnectionString);
            _scopeLifeTime = provider.CreateScope();

            RegisterSubscriptionClientMessageHandler();
        }

        public void Publish(IntegrationEvent @event)
        {
            var eventName = @event.GetType().Name.Replace(INTEGRATION_EVENT_SUFIX, string.Empty);
            var jsonMessage = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var message = new ServiceBusMessage(body)
            {
                MessageId = Guid.NewGuid().ToString(),
            };

            var topicClient = _serviceBusPersisterConnection.CreateModel("");

            topicClient.SendMessageAsync(message)
                .GetAwaiter()
                .GetResult();
        }

        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _subsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFIX, "");

            var containsKey = _subsManager.HasSubscriptionsForEvent<T>();
            if (!containsKey)
            {
                try
                {
                    _subscriptionClient.CreateRuleManager("","").CreateRuleAsync(new RuleDescription
                    {
                        Filter = new CorrelationFilter { Label = eventName },
                        Name = eventName
                    }).GetAwaiter().GetResult();
                }
                catch (ServiceBusException)
                {
                    _logger.LogInformation($"The messaging entity {eventName} already exists.");
                }
            }

            _subsManager.AddSubscription<T, TH>();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFIX, "");

            try
            {
                _subscriptionClient
                 .RemoveRuleAsync(eventName)
                 .GetAwaiter()
                 .GetResult();
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogInformation($"The messaging entity {eventName} Could not be found.");
            }

            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Dispose()
        {
            _subsManager.Clear();
        }

        private void RegisterSubscriptionClientMessageHandler()
        {
            var processor = _subscriptionClient.CreateSessionProcessor("", "");
            processor.ProcessMessageAsync += async (psm) =>
                {
                    var eventName = $"{psm.Identifier}{INTEGRATION_EVENT_SUFIX}";
                    var messageData = Encoding.UTF8.GetString(psm.Message.Body);

                    // Complete the message so that it is not received again.
                    if (await ProcessEvent(eventName, messageData))
                    {
                        await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                    }
                };
            processor.ProcessErrorAsync += ExceptionReceivedHandler;

            processor.StartProcessingAsync().GetAwaiter().GetResult();
        }

        private Task ExceptionReceivedHandler(ProcessErrorEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Entity Path: {exceptionReceivedEventArgs.EntityPath}");
            return Task.CompletedTask;
        }

        private async Task<bool> ProcessEvent(string eventName, string message)
        {
            var processed = false;
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                            if (handler == null) continue;
                            dynamic eventData = JObject.Parse(message);
                            await handler.Handle(eventData);
                        }
                        else
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            if (handler == null) continue;
                            var eventType = _subsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonSerializer.Deserialize(message, eventType);
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                            await (Task)concreteType?.GetMethod("Handle")?.Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
                processed = true;
            }
            return processed;
        }

        private void RemoveDefaultRule()
        {
            try
            {
                _subscriptionClient
                 .RemoveRuleAsync(RuleDescription.DefaultRuleName)
                 .GetAwaiter()
                 .GetResult();
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogInformation($"The messaging entity {RuleDescription.DefaultRuleName} Could not be found.");
            }
        }
    }
}
