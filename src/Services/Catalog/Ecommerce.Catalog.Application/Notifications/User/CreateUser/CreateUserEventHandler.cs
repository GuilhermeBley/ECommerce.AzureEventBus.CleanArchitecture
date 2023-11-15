using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.EventBus.Events;

namespace Ecommerce.Catalog.Application.Notifications.User.CreateUser;

public class CreateUserEventHandler
    : IAppNotificationHandler<CreateUserEvent>
{
    private readonly CatalogContext _catalogContext;

    public CreateUserEventHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task Handle(CreateUserEvent notification, CancellationToken cancellationToken)
    {
        await using var transaction = await _catalogContext.Database.BeginTransactionAsync();

        await _catalogContext.Users.AddAsync(new Model.Identity.UserModel
        {
            Email = notification.Email,
            EmailConfirmed = false,
            Id = notification.Id,
            Name = notification.Name,
            NickName = notification.NickName,
            PasswordHash = string.Empty,
            PasswordSalt = string.Empty,
        });

        await transaction.CommitAsync();

        await _catalogContext.SaveChangesAsync();
    }
}
