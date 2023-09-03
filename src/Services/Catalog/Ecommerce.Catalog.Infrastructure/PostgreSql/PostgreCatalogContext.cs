using Ecommerce.Catalog.Application.Model.Company;
using Ecommerce.Catalog.Application.Model.Identity;
using Ecommerce.Catalog.Application.Model.Product;
using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ecommerce.Catalog.Infrastructure.PostgreSql;

public class PostgreCatalogContext : CatalogContext
{
    private readonly IOptions<PostgresOptions> _options;

    public PostgreCatalogContext(IOptions<PostgresOptions> options)
    {
        _options = options;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .UseNpgsql(
                _options.Value.ConnectionString,
                opt =>
                {
                    opt.MigrationsAssembly("Ecommerce.Catalog.Api");
                });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Application.Model.Identity.RoleClaimModel>(builder =>
        {
            builder.HasKey(x => x.IdClaim);
            builder
                .HasOne<RoleModel>()
                .WithMany()
                .HasForeignKey(x => x.IdRole);
        });

        modelBuilder.Entity<Model.Identity.RoleDbModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.NormalizedName).IsUnique();
        });

        modelBuilder.Entity<Application.Model.Identity.RoleUserClaimModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Application.Model.Identity.UserClaimModel>(builder =>
        {
            builder.HasKey(x => x.IdClaim);
            builder
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Application.Model.Identity.UserModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Application.Model.Company.CompanyModel>(builder =>
        {
            builder.HasKey(x => x.Id);
        });

        modelBuilder.Entity<Application.Model.Company.CompanyProductModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne<ProductModel>()
                .WithMany()
                .HasForeignKey(x => x.ProductId);
        });

        modelBuilder.Entity<Application.Model.Company.CompanyUserClaimModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne<CompanyModel>()
                .WithMany()
                .HasForeignKey(x => x.CompanyId);
            builder
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Application.Model.Product.ProductModel>(builder =>
        {
            builder.HasKey(x => x.Id);
        });
    }
}
