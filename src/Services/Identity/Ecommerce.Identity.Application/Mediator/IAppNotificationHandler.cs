namespace Ecommerce.Identity.Application.Mediator;

public interface IAppNotificationHandler<in T>
{
    Task Handle(T notification, CancellationToken cancellationToken);
}
