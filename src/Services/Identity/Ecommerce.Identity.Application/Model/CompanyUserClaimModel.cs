namespace Ecommerce.Identity.Application.Model;

public class CompanyUserClaimModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
}
