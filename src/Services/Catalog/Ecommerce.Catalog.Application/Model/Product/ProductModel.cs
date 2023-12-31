﻿namespace Ecommerce.Catalog.Application.Model.Product;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Value { get; set; }
    public bool Disabled { get; set; }
    public DateTime UpdateAt { get; set; }
    public DateTime InsertAt { get; set; }
}
