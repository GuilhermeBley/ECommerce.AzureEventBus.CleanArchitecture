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

        modelBuilder.Entity<Application.Model.Identity.UserModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Email).HasColumnType("varchar(500)");
            builder.Property(x => x.Name).HasColumnType("varchar(500)");
            builder.Property(x => x.NickName).HasColumnType("varchar(500)");
            builder.Property(x => x.PasswordHash).HasColumnType("varchar(500)");
            builder.Property(x => x.PasswordSalt).HasColumnType("varchar(500)");
        });

        modelBuilder.Entity<Application.Model.Company.CompanyModel>(builder =>
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("varchar(500)");
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

            builder.Property(x => x.ClaimType).HasColumnType("varchar(255)");
            builder.Property(x => x.ClaimValue).HasColumnType("varchar(255)");
        });

        modelBuilder.Entity<Application.Model.Product.ProductModel>(builder =>
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("varchar(255)");
            builder.Property(x => x.Description).HasColumnType("varchar(1000)");
        });
    }
}
