using Ecommerce.Catalog.Application.Mediator;
using MediatR;

namespace Ecommerce.Catalog.Infrastructure.Mediator;

internal class NotificationHandlerAdapter<TNotification> 
    : INotificationHandler<NotificationAdapter<TNotification>>
{
    private readonly IEnumerable<IAppNotificationHandler<TNotification>> myImpl;

    public NotificationHandlerAdapter(IEnumerable<IAppNotificationHandler<TNotification>> impl)
    {
        myImpl = impl ?? throw new ArgumentNullException(nameof(impl));
    }

    public Task Handle(NotificationAdapter<TNotification> notification, CancellationToken cancellationToken)
    {
        var tasks = myImpl
            .Select(x => x.Handle(notification.Value, cancellationToken));
        return Task.WhenAll(tasks);
    }
}
