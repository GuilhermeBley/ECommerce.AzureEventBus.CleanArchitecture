using Ecommerce.EventBus.Abstractions;
using Ecommerce.EventBus.Events;
using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Application.Security;
using Ecommerce.Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.Company.CreateCompany;

/// <summary>
/// Class contains method handle to execute the company creation of each user
/// </summary>
/// <remarks>
///     <para>If user contains more than specified company quantity, this method will return an error result with message <see cref="ErrorEnum.CompanyQuantityOverflow"/></para>
/// </remarks>
public class CreateCompanyHandler : IAppRequestHandler<CreateCompanyRequest, Result<CreateCompanyResponse>>
{
    private const int MAX_USER_PER_COMAPANY = 10;

    private readonly IEventBus _eventBus;
    private readonly IClaimProvider _claimProvider;
    private readonly IdentityContext _identityContext;

    public CreateCompanyHandler(
        IEventBus eventBus,
        IClaimProvider claimProvider,
        IdentityContext identityContext)
    {
        _eventBus = eventBus;
        _claimProvider = claimProvider;
        _identityContext = identityContext;
    }

    public async Task<Result<CreateCompanyResponse>> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        if (await IsCompanyQuantityOverflowFromCurrentUser())
            return ResultBuilderExtension.CreateFailed<CreateCompanyResponse>(ErrorEnum.CompanyQuantityOverflow);

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

    private async Task<bool> IsCompanyQuantityOverflowFromCurrentUser()
    {
        var userIdResult = (await _claimProvider.GetCurrentAsync())?.GetUserId();

        if (userIdResult is null ||
            !userIdResult.TryGetValue(out var userId))
            return true;

        var companiesFound = await _identityContext
            .CompanyUsersClaims
            .Where(c => c.UserId == userId)
            .CountAsync();

        return companiesFound > MAX_USER_PER_COMAPANY;
    }
}
