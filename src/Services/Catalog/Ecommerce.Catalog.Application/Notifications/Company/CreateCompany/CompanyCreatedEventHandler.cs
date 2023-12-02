using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.EventBus.Events;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Catalog.Application.Notifications.Company.CreateCompany;

/// <summary>
/// Add data to catalog context
/// </summary>
public class CompanyCreatedEventHandler : IIntegrationEventHandler<CompanyCreatedEvent>
{
    private readonly CatalogContext _catalogContext;
    private readonly ILogger _logger;

    public CompanyCreatedEventHandler(
        CatalogContext catalogContext,
        ILogger<CompanyCreatedEventHandler> logger)
    {
        _catalogContext = catalogContext;
        _logger = logger;
    }

    public async Task Handle(CompanyCreatedEvent @event)
    {
        try
        {
            await using var transaction = await _catalogContext.Database.BeginTransactionAsync();

            if (_catalogContext.Companies.FirstOrDefault(c => c.Id == @event.Id) is not null)
                return;

            await _catalogContext.Companies.AddAsync(new Model.Company.CompanyModel
            {
                CreateAt = DateTime.UtcNow,
                Id = @event.Id,
                Name = @event.Name,
                UpdateAt = DateTime.UtcNow,
            });

            await transaction.CommitAsync();

            await _catalogContext.SaveChangesAsync();

            _logger.LogTrace("Company {0} created.", @event.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create company {0}.", @event.Id);

            throw;
        }
    }
}
