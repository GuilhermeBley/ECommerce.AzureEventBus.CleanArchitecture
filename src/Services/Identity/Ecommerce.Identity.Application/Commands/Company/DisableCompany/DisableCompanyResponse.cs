namespace Ecommerce.Identity.Application.Commands.Company.DisableCompany;

public class DisableCompanyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime UpdateAt { get; set; }
    public DateTime CreateAt { get; set; }
}
