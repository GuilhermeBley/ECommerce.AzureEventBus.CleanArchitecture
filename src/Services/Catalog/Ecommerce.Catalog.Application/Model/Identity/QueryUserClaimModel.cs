﻿namespace Ecommerce.Catalog.Application.Model.Identity;

public class QueryUserClaimModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NickName { get; set; }
}
