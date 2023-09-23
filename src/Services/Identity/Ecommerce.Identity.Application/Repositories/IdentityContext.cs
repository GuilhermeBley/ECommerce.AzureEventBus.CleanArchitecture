using Ecommerce.Identity.Application.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Repositories;

public class IdentityContext : DbContext
{
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<RoleModel> Roles { get; set; } = null!;
    public DbSet<RoleClaimModel> RoleClaims { get; set; } = null!;
    public DbSet<UserClaimModel> UserClaims { get; set; } = null!;
    public DbSet<RoleUserClaimModel> RoleUsersClaims { get; set; } = null!;
    public DbSet<CompanyModel> Companies { get; set; } = null!;
    public DbSet<CompanyUserClaimModel> CompanyUsersClaims { get; set; } = null!;
}