using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Infrastructure.Model.Product;
using Ecommerce.Catalog.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ecommerce.Catalog.Infrastructure.PostgreSql;

internal class PostgreContext : CatalogContext
{
    private readonly IOptions<PostgreOptions> _options;

    public PostgreContext(IOptions<PostgreOptions> options)
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
