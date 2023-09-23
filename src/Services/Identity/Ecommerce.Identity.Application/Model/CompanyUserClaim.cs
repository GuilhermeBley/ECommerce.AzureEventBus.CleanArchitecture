namespace Ecommerce.Identity.Application.Model;

public class CompanyUserClaim
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
}
