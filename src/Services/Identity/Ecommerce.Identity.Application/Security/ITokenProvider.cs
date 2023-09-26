using System.Security.Claims;

namespace Ecommerce.Identity.Application.Security;

public interface ITokenProvider
{
    Task<string> CreateTokenAsync(Claim[] claims, CancellationToken cancellationToken = default);
}
