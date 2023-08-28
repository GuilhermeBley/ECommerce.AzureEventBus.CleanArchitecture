using Ecommerce.Catalog.Application.Security;
using System.Security.Claims;

namespace Ecommerce.Catalog.Api.Security;

internal class HttpContextClaimProvider : IClaimProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextClaimProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ClaimsPrincipal?> GetCurrentAsync()
    {
        return await Task.FromResult(
            _httpContextAccessor?.HttpContext?.User
        );
    }
}
