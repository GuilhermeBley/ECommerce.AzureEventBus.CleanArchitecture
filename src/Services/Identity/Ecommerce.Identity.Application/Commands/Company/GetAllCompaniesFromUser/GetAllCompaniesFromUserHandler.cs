using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Application.Security;
using Ecommerce.Identity.Application.Security.Extensions;
using Ecommerce.Identity.Core.Entities;

namespace Ecommerce.Identity.Application.Commands.Company.GetAllCompaniesFromUser;

public class GetAllCompaniesFromUserHandler
    : IAppRequestHandler<GetAllCompaniesFromUserRequest, Result<GetAllCompaniesFromUserResponse>>
{
    private readonly IdentityContext _identityContext;
    private readonly IClaimProvider _claimProvider;

    public GetAllCompaniesFromUserHandler(IdentityContext identityContext, IClaimProvider claimProvider)
    {
        _identityContext = identityContext;
        _claimProvider = claimProvider;
    }

    public async Task<Result<GetAllCompaniesFromUserResponse>> Handle(GetAllCompaniesFromUserRequest request, CancellationToken cancellationToken)
    {
        var companyId = (await _claimProvider.GetCurrentAsync())?.GetCompany();

        if (companyId is null)
            return ResultBuilderExtension.CreateFailed<GetAllCompaniesFromUserResponse>(ErrorEnum.Unauthorized);

        var queryOnlyActivatedCompanies =
            _identityContext.Companies
                .Where(c => !c.Disabled)
                .Where(c => c.Id == companyId.Value)
                .Select(c => new GetAllCompaniesFromUserResponse.CompanyResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    UpdateAt = c.UpdateAt
                });

        return await Task.FromResult(
            Result.Success(
                new GetAllCompaniesFromUserResponse(queryOnlyActivatedCompanies)
        ));
    }
}
