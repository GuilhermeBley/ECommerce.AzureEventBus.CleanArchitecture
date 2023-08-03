using Ecommerce.Catalog.Infrastructure.Model.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Catalog.Infrastructure.PostgreSql;

internal class PostgreContext : DbContext
{
    private readonly IConfigurationSection _section;

    public DbSet<RoleClaimDbModel> RoleClaims { get; set; } = null!;
    public DbSet<RoleDbModel> Roles { get; set; } = null!;
    public DbSet<RoleUserClaimDbModel> RoleUserClaims { get; set; } = null!;
    public DbSet<UserClaimDbModel> UserClaims { get; set; } = null!;
    public DbSet<UserDbModel> Users { get; set; } = null!;

    public PostgreContext(IConfigurationSection section)
    {
        _section = section;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .UseNpgsql(
                _section["connectionString"],
                opt =>
                {
                });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RoleClaimDbModel>(builder =>
        {

        });

        modelBuilder.Entity<RoleDbModel>(builder =>
        {

        });

        modelBuilder.Entity<RoleUserClaimDbModel>(builder =>
        {

        });

        modelBuilder.Entity<UserClaimDbModel>(builder =>
        {

        });

        modelBuilder.Entity<UserDbModel>(builder =>
        {

        });
    }
}
