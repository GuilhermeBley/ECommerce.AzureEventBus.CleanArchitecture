using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.EventBus.Events;

namespace Ecommerce.Catalog.Application.Notifications.Company.CreateCompany;

/// <summary>
/// Add data to catalog context
/// </summary>
public class CompanyCreatedEventHandler : IIntegrationEventHandler<CompanyCreatedEvent>
{
    private readonly CatalogContext _catalogContext;

    public CompanyCreatedEventHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task Handle(CompanyCreatedEvent @event)
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
    }
}
