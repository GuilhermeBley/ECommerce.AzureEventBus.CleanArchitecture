namespace Ecommerce.Identity.Application.Model;

public class CompanyModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? UpdateAt { getprivate set; }
    public DateTime CreateAt { get; set; }
}
