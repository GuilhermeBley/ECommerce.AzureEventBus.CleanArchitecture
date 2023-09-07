﻿using Ecommerce.Catalog.Application.Model.Company;
using Ecommerce.Catalog.Application.Model.Identity;
using Ecommerce.Catalog.Application.Model.Product;
using Ecommerce.Catalog.Core.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Application.Repositories;

public class CatalogContext : DbContext
{
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<RoleModel> Roles { get; set; } = null!;
    public DbSet<RoleClaimModel> RoleClaims { get; set; } = null!;
    public DbSet<UserClaimModel> UserClaims { get; set; } = null!;
    public DbSet<RoleUserClaimModel> RoleUsersClaims { get; set; } = null!;
    public DbSet<ProductModel> Products { get; set; } = null!;
    public DbSet<CompanyModel> Companies { get; set; } = null!;
    public DbSet<CompanyUserClaimModel> CompanyUsersClaims { get; set; } = null!;
    public DbSet<CompanyProductModel> CompanyProducts { get; set; } = null!;
}
