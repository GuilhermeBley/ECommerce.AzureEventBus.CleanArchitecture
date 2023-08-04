﻿namespace Ecommerce.Catalog.Infrastructure.Model.Company;

public class CompanyUserClaimDbModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
