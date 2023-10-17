using MediatR;

namespace Ecommerce.Identity.Infrastructure.Mediator;

internal class NotificationAdapter<TNotification> : INotification
{
    public NotificationAdapter(TNotification value)
    {
        Value = value;
    }

    public TNotification Value { get; }
}
