using Ecommerce.Catalog.Application.Model.Identity;
using Ecommerce.Catalog.Application.Model.Product;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Application.Repositories;

public interface ICatalogContext
{
    DbSet<UserModel> Users { get; }
    DbSet<RoleModel> Roles { get; }
    DbSet<RoleClaimModel> RoleClaims { get; }
    DbSet<UserClaimModel> UserClaims { get; }
    DbSet<ProductModel> Products { get; }
}
