using Ecommerce.Identity.Application.Mediator;

namespace Ecommerce.Identity.Application.Commands.Company.DisableCompany;

public class DisableCompanyHandler
    : IAppRequestHandler<DisableCompanyRequest, Result<DisableCompanyResponse>>
{
    public Task<Result<DisableCompanyResponse>> Handle(DisableCompanyRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
