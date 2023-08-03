using Ecommerce.Catalog.Infrastructure.Model.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static System.Collections.Specialized.BitVector32;

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
    }
}
