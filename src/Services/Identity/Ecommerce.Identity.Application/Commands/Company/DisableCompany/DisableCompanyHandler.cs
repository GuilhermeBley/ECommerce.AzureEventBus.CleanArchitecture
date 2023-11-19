using Ecommerce.EventBus.Abstractions;
using Ecommerce.EventBus.Events;
using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.Company.DisableCompany;

public class DisableCompanyHandler
    : IAppRequestHandler<DisableCompanyRequest, Result<DisableCompanyResponse>>
{
    private readonly IEventBus _eventBus;
    private readonly IdentityContext _identityContext;

    public DisableCompanyHandler(
        IEventBus eventBus,
        IdentityContext identityContext)
    {
        _eventBus = eventBus;
        _identityContext = identityContext;
    }

    public async Task<Result<DisableCompanyResponse>> Handle(DisableCompanyRequest request, CancellationToken cancellationToken)
    {
        var companyFound =
            await _identityContext.Companies.FirstOrDefaultAsync(c => c.Id == request.CompanyId);

        if (companyFound is null)
            return ResultBuilderExtension.CreateFailed<DisableCompanyResponse>(ErrorEnum.CompanyNotFound);

        companyFound.Disabled = request.Disabled;
        companyFound.UpdateAt = DateTime.UtcNow;

        await _identityContext.SaveChangesAsync(cancellationToken);

        await _eventBus.PublishAsync(new CompanyDisabledEvent
        {
            CreateAt = companyFound.CreateAt,
            Name = companyFound.Name,
            Id = companyFound.Id,
            Disabled = companyFound.Disabled,
            UpdateAt = companyFound.UpdateAt.Value,
        });

        return Result.Success(new DisableCompanyResponse
        {
            CreateAt = companyFound.CreateAt,
            UpdateAt = companyFound.UpdateAt.Value,
            Id = companyFound.Id,
            Name = companyFound.Name,
        });
    }
}
