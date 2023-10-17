using Ecommerce.Identity.Application.Model;
using Ecommerce.Identity.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Infrastructure.Context;

public class MySqlDbContext : IdentityContext
{
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
        });

        modelBuilder.Entity<Application.Model.RoleModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.NormalizedName).IsUnique();
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
        });

        modelBuilder.Entity<Application.Model.UserModel>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Application.Model.CompanyModel>(builder =>
        {
            builder.HasKey(x => x.Id);
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
