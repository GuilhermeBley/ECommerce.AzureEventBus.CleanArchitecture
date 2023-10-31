using Ecommerce.Identity.Application.Model;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ecommerce.Identity.Infrastructure.Context;

public class MySqlDbContext : IdentityContext
{
    private readonly IOptions<MySqlOptions> _options;

    public MySqlDbContext(IOptions<MySqlOptions> options)
    {
        _options = options;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .UseMySql(
                _options.Value.ConnectionString,
                ServerVersion.AutoDetect(_options.Value.ConnectionString),
                opt =>
                {
                    opt.MigrationsAssembly("Ecommerce.Identity.Api");
                });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Application.Model.RoleClaimModel>(builder =>
        {
            builder.HasKey(x => x.IdClaim);
            builder
                .HasOne<RoleModel>()
                .WithMany()
                .HasForeignKey(x => x.IdRole);
            builder.Property(p => p.ClaimType).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(p => p.ClaimValue).HasColumnType("VARCHAR").HasMaxLength(255);
        });

        modelBuilder.Entity<Application.Model.RoleModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.NormalizedName).IsUnique();
            builder.Property(p => p.Name).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(p => p.NormalizedName).HasColumnType("VARCHAR").HasMaxLength(255);
        });

        modelBuilder.Entity<Application.Model.RoleUserClaimModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Application.Model.UserClaimModel>(builder =>
        {
            builder.HasKey(x => x.IdClaim);
            builder
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(x => x.UserId);
            builder.Property(p => p.ClaimType).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(p => p.ClaimValue).HasColumnType("VARCHAR").HasMaxLength(255);
        });

        modelBuilder.Entity<Application.Model.UserModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(p => p.Name).HasColumnType("VARCHAR").HasMaxLength(500);
            builder.Property(p => p.NickName).HasColumnType("VARCHAR").HasMaxLength(500);
            builder.Property(p => p.Email).HasColumnType("VARCHAR").HasMaxLength(500);
            builder.Property(p => p.PhoneNumber).HasColumnType("VARCHAR").HasMaxLength(50);
            builder.Property(p => p.PasswordHash).HasColumnType("VARCHAR").HasMaxLength(255);
            builder.Property(p => p.PasswordSalt).HasColumnType("VARCHAR").HasMaxLength(255);
        });

        modelBuilder.Entity<Application.Model.CompanyModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Name).HasColumnType("VARCHAR").HasMaxLength(500);
        });

        modelBuilder.Entity<Application.Model.CompanyUserClaimModel>(builder =>
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
    }
}
