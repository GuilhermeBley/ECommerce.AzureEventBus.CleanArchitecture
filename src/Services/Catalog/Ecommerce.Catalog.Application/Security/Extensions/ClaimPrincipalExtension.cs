using Ecommerce.Catalog.Core.Exceptions;
using Ecommerce.Catalog.Core.Extension;
using Ecommerce.Catalog.Core.Security;
using System.Security.Claims;

namespace Ecommerce.Catalog.Application.Security.Extensions;

internal static class ClaimPrincipalExtension
{
    public static ResultBase ContainsRole(this ClaimsPrincipal claimsPrincipal, string roleValue)
    {
        if (IsLogged(claimsPrincipal).IsFailure)
            return ResultBuilderExtension.CreateFailed(ErrorEnum.Unauthorized);

        var claimFound = claimsPrincipal.FindAll(c => c.Type == ClaimTypeCore.DEFAULT_ROLE && c.Value == roleValue);

        if (claimFound is null)
            return ResultBuilderExtension.CreateFailed(ErrorEnum.Unauthorized);

        return ResultBuilder.CreateSuccess();
    }

    public static Result<string> GetClaimValue(this ClaimsPrincipal claimsPrincipal, string type)
    {
        if (IsLogged(claimsPrincipal).IsFailure)
            return ResultBuilderExtension.CreateFailed<string>(ErrorEnum.Unauthorized);

        var claimFound = claimsPrincipal.FindFirst(type);

        if (claimFound is null)
            return ResultBuilderExtension.CreateFailed<string>(ErrorEnum.Unauthorized);

        return ResultBuilder<string>.CreateSuccess(claimFound.Value);
    }

    public static ResultBase IsLogged(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal.HasClaim(c => c.Type == ClaimTypeCore.DEFAULT_ID))
            return ResultBase.Success();

        return ResultBuilderExtension.CreateFailed(ErrorEnum.Unauthorized); 
    }
}
