namespace Ecommerce.Identity.Infrastructure.Options;

public class MySqlOptions
{
    public const string SECTION = "MySql";

    public string ConnectionString { get; set; } = string.Empty;
}
