namespace Ecommerce.Identity.Application.Commands.Company.CreateCompany;

public class CreateCompanyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? UpdateAt { get; set; }
}
