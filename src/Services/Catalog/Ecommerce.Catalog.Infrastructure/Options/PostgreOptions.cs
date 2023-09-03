namespace Ecommerce.Catalog.Infrastructure.Options;

public class PostgreOptions
{
    public const string SECTION = "Postgres";

    public string ConnectionString { get; set; } = string.Empty;
}
