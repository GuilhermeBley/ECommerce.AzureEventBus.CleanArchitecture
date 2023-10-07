using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.Company.UpdateCompanyName;

public class UpdateCompanyNameHandler : IAppRequestHandler<UpdateCompanyNameRequest, Result<UpdateCompanyNameResponse>>
{
    private readonly IdentityContext _identityContext;

    public UpdateCompanyNameHandler(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }

    public async Task<Result<UpdateCompanyNameResponse>> Handle(UpdateCompanyNameRequest request, CancellationToken cancellationToken)
    {
        var companyFound = await _identityContext.Companies.FirstOrDefaultAsync(c => c.Id == request.CompanyId);

        if (companyFound is null)
            return ResultBuilderExtension.CreateFailed<UpdateCompanyNameResponse>(ErrorEnum.CompanyNotFound);

        var companyResult = Core.Entities.Company.Create(
            id: companyFound.Id,
            name: request.NewName,
            updateAt: DateTime.UtcNow,
            createAt: companyFound.CreateAt
        );

        if (!companyResult.TryGetValue(out var companyEntity))
            return Result<UpdateCompanyNameResponse>.Failed(companyResult.Errors);

        companyFound.UpdateAt = companyEntity.UpdateAt;
        companyFound.Name = companyEntity.Name;

        await _identityContext.SaveChangesAsync();

        return Result<UpdateCompanyNameResponse>.Success(
            new UpdateCompanyNameResponse
            {
                CreateAt = companyFound.CreateAt,
                Id = companyFound.Id,
                Name = companyFound.Name,
                UpdateAt = companyFound.UpdateAt!.Value
            }
        );
    }
}
