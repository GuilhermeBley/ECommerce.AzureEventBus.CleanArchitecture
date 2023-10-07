namespace Ecommerce.Identity.Application.Commands.Company.UpdateCompanyName;

public class UpdateCompanyNameResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime UpdateAt { get; set; }
    public DateTime CreateAt { get; set; }
}
