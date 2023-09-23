using System.Security.Claims;

namespace Ecommerce.Identity.Application.Security;

public interface IClaimProvider
{
    Task<ClaimsPrincipal?> GetCurrentAsync();
}
