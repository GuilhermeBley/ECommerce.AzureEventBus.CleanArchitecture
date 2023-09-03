namespace Ecommerce.Catalog.Infrastructure.Options;

public class PostgresOptions
{
    public const string SECTION = "Postgres";

    public string ConnectionString { get; set; } = string.Empty;
}
