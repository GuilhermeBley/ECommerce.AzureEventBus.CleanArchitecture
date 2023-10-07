using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Core.Entities;

namespace Ecommerce.Identity.Application.Commands.Company.CreateCompany;

public class CreateCompanyHandler : IAppRequestHandler<CreateCompanyRequest, Result<CreateCompanyResponse>>
{
    private readonly IdentityContext _identityContext;

    public CreateCompanyHandler(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }

    public async Task<Result<CreateCompanyResponse>> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var companyResult = Core.Entities.Company.Create(
            id: Guid.NewGuid(),
            name: request.Name,
            updateAt: DateTime.UtcNow,
            createAt: DateTime.UtcNow
        );

        if (!companyResult.TryGetValue(out var companyEntity))
            return Result<CreateCompanyResponse>.Failed(companyResult.Errors);

        var companyCreated = await _identityContext.Companies.AddAsync(
            new Model.CompanyModel
            {
                CreateAt = companyEntity.CreateAt,
                Id = companyEntity.Id,
                Name = companyEntity.Name,
                UpdateAt = companyEntity.UpdateAt,
            }
        );

        await _identityContext.SaveChangesAsync();

        return Result<CreateCompanyResponse>.Success(
            new CreateCompanyResponse
            {
                UpdateAt = companyCreated.Entity.UpdateAt,
                Name = companyCreated.Entity.Name,
                Id = companyCreated.Entity.Id
            }  
        );
    }
}
