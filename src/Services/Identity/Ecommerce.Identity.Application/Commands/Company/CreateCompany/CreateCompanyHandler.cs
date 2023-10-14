using Ecommerce.EventBus.Abstractions;
using Ecommerce.EventBus.Events;
using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Core.Entities;

namespace Ecommerce.Identity.Application.Commands.Company.CreateCompany;

public class CreateCompanyHandler : IAppRequestHandler<CreateCompanyRequest, Result<CreateCompanyResponse>>
{
    private readonly IEventBus _eventBus;
    private readonly IdentityContext _identityContext;

    public CreateCompanyHandler(
        IEventBus eventBus,
        IdentityContext identityContext)
    {
        _eventBus = eventBus;
        _identityContext = identityContext;
    }

    public async Task<Result<CreateCompanyResponse>> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var companyResult = Core.Entities.Company.Create(
            id: Guid.NewGuid(),
            name: request.Name,
            updateAt: DateTime.UtcNow,
            createAt: DateTime.UtcNow
        );

        if (!companyResult.TryGetValue(out var companyEntity))
            return Result<CreateCompanyResponse>.Failed(companyResult.Errors);

        var companyCreated = await _identityContext.Companies.AddAsync(
            new Model.CompanyModel
            {
                CreateAt = companyEntity.CreateAt,
                Id = companyEntity.Id,
                Name = companyEntity.Name,
                UpdateAt = companyEntity.UpdateAt,
            }
        );

        await _identityContext.SaveChangesAsync();

        await _eventBus.PublishAsync(new CompanyCreatedEvent
        {
            Name = companyEntity.Name,
            Id = companyEntity.Id,
            CreateAt = companyEntity.CreateAt,
            UpdateAt = companyEntity.UpdateAt ?? DateTime.UtcNow,
        });

        return Result<CreateCompanyResponse>.Success(
            new CreateCompanyResponse
            {
                UpdateAt = companyCreated.Entity.UpdateAt,
                Name = companyCreated.Entity.Name,
                Id = companyCreated.Entity.Id
            }  
        );
    }
}
