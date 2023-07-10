namespace Ecommerce.Catalog.Application.Mediator;

public interface IAppNotificationHandler<in T>
{
    Task Handle(T notification, CancellationToken cancellationToken);
}
