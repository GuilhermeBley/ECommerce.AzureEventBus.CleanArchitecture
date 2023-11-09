namespace Ecommerce.Identity.Application.Commands.Company.GetAllCompaniesFromUser;

public class GetAllCompaniesFromCurrentUserResponse
{
    public IQueryable<CompanyResponse> Companies { get; }

    public GetAllCompaniesFromCurrentUserResponse(IQueryable<CompanyResponse> companies)
    {
        Companies = companies;
    }

    public class CompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? UpdateAt { get; set; }
    }
}