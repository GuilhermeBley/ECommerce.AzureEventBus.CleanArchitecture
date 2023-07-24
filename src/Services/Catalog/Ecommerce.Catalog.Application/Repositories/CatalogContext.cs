using Ecommerce.Catalog.Application.Model.Identity;
using Ecommerce.Catalog.Application.Model.Product;
using Ecommerce.Catalog.Core.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Application.Repositories;

public class CatalogContext : DbContext
{
    DbSet<UserModel> Users { get; set; } = null!;
    DbSet<RoleModel> Roles { get; set; } = null!;
    DbSet<RoleClaimModel> RoleClaims { get; set; } = null!;
    DbSet<UserClaimModel> UserClaims { get; set; } = null!;
    DbSet<RoleUserClaimModel> RoleUsersClaims { get; set; } = null!;
    DbSet<ProductModel> Products { get; set; } = null!;

    DbSet<Company> Companies { get; } = null!;
    DbSet<CompanyUserClaim> CompanyUsersClaims { get; } = null!;
}
