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
            builder.HasKey(x => x.IdClaim);
            builder
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.IdRole);
        });

        modelBuilder.Entity<Model.Identity.RoleDbModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.NormalizedName).IsUnique();
        });

        modelBuilder.Entity<Model.Identity.RoleUserClaimDbModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Model.Identity.UserClaimDbModel>(builder =>
        {
            builder.HasKey(x => x.IdClaim);
            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Model.Identity.UserDbModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Model.Company.CompanyDbModel>(builder =>
        {
            builder.HasKey(x => x.Id);
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
            builder.HasKey(x => x.Id);
            builder
                .HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId);
            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Model.Product.ProductDbModel>(builder =>
        {
            builder.HasKey(x => x.Id);
        });
    }
}
