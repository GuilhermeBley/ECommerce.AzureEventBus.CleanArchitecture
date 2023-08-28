using System.Security.Claims;

namespace Ecommerce.Catalog.Application.Security;

public interface IClaimProvider
{
    Task<ClaimsPrincipal?> GetCurrentAsync();
}
