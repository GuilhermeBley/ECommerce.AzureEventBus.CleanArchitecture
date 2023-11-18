namespace Ecommerce.Identity.Api.Options;

public class JwtOptions
{
    public const string SECTION = "Jwt";

    public string Secret { get; set; } = string.Empty;
}
