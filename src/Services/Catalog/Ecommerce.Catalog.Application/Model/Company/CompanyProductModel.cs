namespace Ecommerce.Catalog.Application.Model.Company;

public class CompanyProductModel
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime CreateAt { get; set; }
}
