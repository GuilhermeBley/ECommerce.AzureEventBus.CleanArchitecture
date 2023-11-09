using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Application.Security;
using Ecommerce.Identity.Application.Security.Extensions;
using Ecommerce.Identity.Core.Entities;

namespace Ecommerce.Identity.Application.Commands.Company.GetAllCompaniesFromUser;

public class GetAllCompaniesFromCurrentUserHandler
    : IAppRequestHandler<GetAllCompaniesFromCurrentUserRequest, Result<GetAllCompaniesFromCurrentUserResponse>>
{
    private readonly IdentityContext _identityContext;
    private readonly IClaimProvider _claimProvider;

    public GetAllCompaniesFromCurrentUserHandler(IdentityContext identityContext, IClaimProvider claimProvider)
    {
        _identityContext = identityContext;
        _claimProvider = claimProvider;
    }

    public async Task<Result<GetAllCompaniesFromCurrentUserResponse>> Handle(GetAllCompaniesFromCurrentUserRequest request, CancellationToken cancellationToken)
    {
        var userId = (await _claimProvider.GetCurrentAsync())?.GetUserId();

        if (userId is null)
            return ResultBuilderExtension.CreateFailed<GetAllCompaniesFromCurrentUserResponse>(ErrorEnum.Unauthorized);

        var queryOnlyActivatedCompanies =
            from company in _identityContext.Companies
            join userCompany in _identityContext.CompanyUsersClaims
                on company.Id equals userCompany.CompanyId
            where !company.Disabled
            select new GetAllCompaniesFromCurrentUserResponse.CompanyResponse
            {
                Id = company.Id,
                Name = company.Name,
                UpdateAt = company.UpdateAt
            };

        return await Task.FromResult(
            Result.Success(
                new GetAllCompaniesFromCurrentUserResponse(queryOnlyActivatedCompanies)
        ));
    }
}
