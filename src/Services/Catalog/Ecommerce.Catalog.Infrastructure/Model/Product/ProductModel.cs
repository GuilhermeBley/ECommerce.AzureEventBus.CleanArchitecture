﻿namespace Ecommerce.Catalog.Infrastructure.Model.Product;

public class ProductDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Value { get; set; }
    public DateTime UpdateAt { get; set; }
    public DateTime InsertAt { get; set; }
}
