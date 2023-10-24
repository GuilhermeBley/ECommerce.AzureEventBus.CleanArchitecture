namespace Ecommerce.Identity.Api.Options;

public record JwtOptions(
    string Secret)
{
    public const string SECTION = "Jwt";
}
