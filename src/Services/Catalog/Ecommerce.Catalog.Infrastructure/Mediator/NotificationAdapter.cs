using MediatR;

namespace Ecommerce.Catalog.Infrastructure.Mediator;

internal class NotificationAdapter<TNotification> : INotification
{
    public NotificationAdapter(TNotification value)
    {
        Value = value;
    }

    public TNotification Value { get; }
}
