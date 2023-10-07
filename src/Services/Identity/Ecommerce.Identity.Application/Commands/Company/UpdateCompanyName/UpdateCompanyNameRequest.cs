namespace Ecommerce.Identity.Application.Commands.Company.UpdateCompanyName;

public class UpdateCompanyNameRequest
{
    public Guid CompanyId { get; set; }
    public string NewName { get; set; } = string.Empty;
}
