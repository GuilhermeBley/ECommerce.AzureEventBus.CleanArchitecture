using Ecommerce.Catalog.Infrastructure.Model.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Catalog.Infrastructure.PostgreSql;

internal class PostgreContext : DbContext
{
    private readonly IConfigurationSection _section;

    public DbSet<Model.Identity.RoleClaimDbModel> RoleClaims { get; set; } = null!;
    public DbSet<Model.Identity.RoleDbModel> Roles { get; set; } = null!;
    public DbSet<Model.Identity.RoleUserClaimDbModel> RoleUserClaims { get; set; } = null!;
    public DbSet<Model.Identity.UserClaimDbModel> UserClaims { get; set; } = null!;
    public DbSet<Model.Identity.UserDbModel> Users { get; set; } = null!;

    public DbSet<Model.Company.CompanyDbModel> Companies { get; set; } = null!;
    public DbSet<Model.Company.CompanyProductDbModel> CompanyProducts { get; set; } = null!;
    public DbSet<Model.Company.CompanyUserClaimDbModel> CompanyUserClaims { get; set; } = null!;

    public DbSet<Model.Product.ProductDbModel> Products { get; set; } = null!;

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

        modelBuilder.Entity<Model.Identity.RoleClaimDbModel>(builder =>
        {

        });

        modelBuilder.Entity<Model.Identity.RoleDbModel>(builder =>
        {

        });

        modelBuilder.Entity<Model.Identity.RoleUserClaimDbModel>(builder =>
        {

        });

        modelBuilder.Entity<Model.Identity.UserClaimDbModel>(builder =>
        {

        });

        modelBuilder.Entity<Model.Identity.UserDbModel>(builder =>
        {

        });

        modelBuilder.Entity<Model.Company.CompanyDbModel>(builder =>
        {

        });

        modelBuilder.Entity<Model.Company.CompanyProductDbModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

        });

        modelBuilder.Entity<Model.Company.CompanyUserClaimDbModel>(builder =>
        {

        });

        modelBuilder.Entity<Model.Product.ProductDbModel>(builder =>
        {
            builder.HasKey(x => x.Id);
        });
    }
}
