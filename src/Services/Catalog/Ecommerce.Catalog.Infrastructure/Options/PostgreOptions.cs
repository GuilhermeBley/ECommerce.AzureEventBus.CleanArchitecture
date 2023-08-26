namespace Ecommerce.Catalog.Infrastructure.Options;

public class PostgreOptions
{
    public const string SECTION = "Postgre";

    public string ConnectionString { get; set; } = string.Empty;
}
