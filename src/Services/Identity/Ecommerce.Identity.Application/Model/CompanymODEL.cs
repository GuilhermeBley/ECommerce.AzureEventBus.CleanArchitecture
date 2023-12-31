﻿namespace Ecommerce.Identity.Application.Model;

public class CompanyModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Disabled { get; set; }
    public DateTime? UpdateAt { get; set; }
    public DateTime CreateAt { get; set; }
}
