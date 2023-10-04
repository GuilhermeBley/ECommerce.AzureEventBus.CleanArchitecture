using Ecommerce.Identity.Application.Mediator;

namespace Ecommerce.Identity.Application.Commands.Company.CreateCompany;

public class CreateCompanyHandler : IAppRequestHandler<CreateCompanyRequest, Result<CreateCompanyResponse>>
{
    public Task<Result<CreateCompanyResponse>> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
