namespace Ecommerce.Catalog.Application.Model.Identity;

public class QueryUserRoleModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NickName { get; set; }
    IEnumerable<RoleModel> Roles { get; set; } = Enumerable.Empty<RoleModel>();
}
