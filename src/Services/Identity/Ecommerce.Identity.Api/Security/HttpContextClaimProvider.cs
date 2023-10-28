using Ecommerce.Identity.Application.Security;
using System.Security.Claims;

namespace Ecommerce.Identity.Api.Security
{
    internal class HttpContextClaimProvider : IClaimProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextClaimProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<ClaimsPrincipal?> GetCurrentAsync()
            => await Task.FromResult(
                _contextAccessor.HttpContext?.User
            );
    }
}
