namespace Ecommerce.Catalog.Application.Model.Product;

public class QueryProductCompany
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public double ProductValue { get; set; }
    public DateTime ProductInsertAt { get; set; }

    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public DateTime CompanyCreateAt { get; set; }
}
