namespace Ecommerce.Catalog.Infrastructure.Model.Identity;

public class RoleDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
}
