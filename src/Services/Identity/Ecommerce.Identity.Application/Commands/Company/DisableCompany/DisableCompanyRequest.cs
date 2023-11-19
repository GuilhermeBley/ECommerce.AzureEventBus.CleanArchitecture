namespace Ecommerce.Identity.Application.Commands.Company.DisableCompany;

public class DisableCompanyRequest
{
    public Guid CompanyId { get; set; }
    public bool Disabled { get; set; }
}
