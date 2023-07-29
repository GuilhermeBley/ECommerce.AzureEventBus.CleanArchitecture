using Ecommerce.Catalog.Core.Exceptions;
using Ecommerce.Catalog.Core.Extension;
using Ecommerce.Catalog.Core.Security;
using System.Security.Claims;

namespace Ecommerce.Catalog.Application.Security.Extensions;

internal static class ClaimPrincipalExtension
{
    public static Result<Guid> GetCompany(this ClaimsPrincipal claimsPrincipal)
    {
        if (IsLogged(claimsPrincipal).IsFailure)
            return ResultBuilderExtension.CreateFailed<Guid>(ErrorEnum.Unauthorized);

        var claimsFound = claimsPrincipal
            .FindAll(c => c.Type == ClaimTypeCore.DEFAULT_COMPANY_ID);

        if (claimsFound is null || !claimsFound.Any())
            return ResultBuilderExtension.CreateFailed<Guid>(ErrorEnum.Forbbiden);

        var containsGuid = Guid.TryParse(claimsFound.FirstOrDefault()?.Value, out Guid resultGuid);

        if (!containsGuid)
            return ResultBuilderExtension.CreateFailed<Guid>(ErrorEnum.Forbbiden);

        return ResultBuilder<Guid>.CreateSuccess(resultGuid);
    }

    public static Result ContainsRole(this ClaimsPrincipal claimsPrincipal, string roleValue)
    {
        if (IsLogged(claimsPrincipal).IsFailure)
            return ResultBuilderExtension.CreateFailed(ErrorEnum.Unauthorized);

        var claimsFound = claimsPrincipal.FindAll(c => c.Type == ClaimTypeCore.DEFAULT_ROLE && c.Value == roleValue);

        if (claimsFound is null || !claimsFound.Any())
            return ResultBuilderExtension.CreateFailed(ErrorEnum.Forbbiden);

        return ResultBuilder.CreateSuccess();
    }

    public static Result<string> GetClaimValue(this ClaimsPrincipal claimsPrincipal, string type)
    {
        if (IsLogged(claimsPrincipal).IsFailure)
            return ResultBuilderExtension.CreateFailed<string>(ErrorEnum.Unauthorized);

        var claimFound = claimsPrincipal.FindFirst(type);

        if (claimFound is null)
            return ResultBuilderExtension.CreateFailed<string>(ErrorEnum.Forbbiden);

        return ResultBuilder<string>.CreateSuccess(claimFound.Value);
    }

    public static Result IsLogged(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal.HasClaim(c => c.Type == ClaimTypeCore.DEFAULT_ID))
            return Result.Success();

        return ResultBuilderExtension.CreateFailed(ErrorEnum.Unauthorized); 
    }
}
