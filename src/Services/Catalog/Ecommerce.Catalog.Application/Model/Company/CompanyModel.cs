namespace Ecommerce.Catalog.Application.Model.Company;

public class CompanyModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? UpdateAt { get; set; }
    public DateTime CreateAt { get; set; }
}
