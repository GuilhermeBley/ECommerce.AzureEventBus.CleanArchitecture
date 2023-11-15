using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.EventBus.Events;

namespace Ecommerce.Catalog.Application.Notifications.User.CreateUser;

public class CreateUserEventHandler
    : IIntegrationEventHandler<CreateUserEvent>
{
    private readonly CatalogContext _catalogContext;

    public CreateUserEventHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task Handle(CreateUserEvent @event)
    {
        await using var transaction = await _catalogContext.Database.BeginTransactionAsync();

        await _catalogContext.Users.AddAsync(new Model.Identity.UserModel
        {
            Email = @event.Email,
            EmailConfirmed = false,
            Id = @event.Id,
            Name = @event.Name,
            NickName = @event.NickName,
            PasswordHash = string.Empty,
            PasswordSalt = string.Empty,
        });

        await transaction.CommitAsync();

        await _catalogContext.SaveChangesAsync();
    }
}
