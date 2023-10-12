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
        var userId = (await _claimProvider.GetCurrentAsync())?.GetUserId();

        if (userId is null)
            return ResultBuilderExtension.CreateFailed<GetAllCompaniesFromUserResponse>(ErrorEnum.Unauthorized);

        var queryOnlyActivatedCompanies =
            from company in _identityContext.Companies
            join userCompany in _identityContext.CompanyUsersClaims
                on company.Id equals userCompany.CompanyId
            where !company.Disabled
            select new GetAllCompaniesFromUserResponse.CompanyResponse
            {
                Id = company.Id,
                Name = company.Name,
                UpdateAt = company.UpdateAt
            };

        return await Task.FromResult(
            Result.Success(
                new GetAllCompaniesFromUserResponse(queryOnlyActivatedCompanies)
        ));
    }
}
