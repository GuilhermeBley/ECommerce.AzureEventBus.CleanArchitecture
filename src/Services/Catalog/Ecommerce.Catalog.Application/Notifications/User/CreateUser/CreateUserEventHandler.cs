using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.EventBus.Events;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Catalog.Application.Notifications.User.CreateUser;

public class CreateUserEventHandler
    : IIntegrationEventHandler<CreateUserEvent>
{
    private readonly CatalogContext _catalogContext;
    private readonly ILogger _logger;

    public CreateUserEventHandler(
        CatalogContext catalogContext, 
        ILogger<CreateUserEventHandler> logger)
    {
        _catalogContext = catalogContext;
        _logger = logger;
    }

    public async Task Handle(CreateUserEvent @event)
    {
        try
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

            _logger.LogTrace("User {0} created.", @event.Id);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Failed to create company {0}.", @event.Id);

            throw;
        }
    }
}
