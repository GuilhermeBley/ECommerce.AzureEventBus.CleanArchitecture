namespace Ecommerce.Catalog.Infrastructure.Model.Company;

public class CompanyProductDbModel
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid ProductId { get; set; }
    public DateTime CreateAt { get; set; }
}
