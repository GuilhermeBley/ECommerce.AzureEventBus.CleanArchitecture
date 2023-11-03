using Ecommerce.Identity.Api.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Identity.Api.Service;

internal interface  ITokenService
{
    string GenerateToken(Claim[] claims, TimeSpan? expiresTime = null);
}

internal class TokenService : ITokenService
{
    private static readonly TimeSpan DefaultTimeToExpiress = TimeSpan.FromHours(2);

    private readonly IOptions<JwtOptions> _options;

    public TokenService(IOptions<Options.JwtOptions> options)
    {
        _options = options;
    }

    public string GenerateToken(Claim[] claims, TimeSpan? expiresTime = null)
    {
        expiresTime = expiresTime ?? DefaultTimeToExpiress;
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Value.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(expiresTime.Value),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
