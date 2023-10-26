namespace Ecommerce.Identity.Infrastructure.Options;

public record MySqlOptions(string ConnectionString)
{
    public const string SECTION = "MySql";
}
