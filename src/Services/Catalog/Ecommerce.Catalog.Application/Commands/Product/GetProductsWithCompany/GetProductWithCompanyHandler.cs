using Ecommerce.Catalog.Application.Model.Product;
using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Application.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Application.Commands.Product.GetProductsWithCompany;

public class GetProductWithCompanyHandler
    : IAppRequestHandler<GetProductWithCompanyRequest, Result<IQueryable<QueryProductCompany>>>
{
    private readonly CatalogContext _catalogContext;
    private readonly IClaimProvider _claimProvider;

    public GetProductWithCompanyHandler(IClaimProvider principal, CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
        _claimProvider = principal;
    }

    public async Task<Result<IQueryable<QueryProductCompany>>> Handle(GetProductWithCompanyRequest request, 
        CancellationToken cancellationToken)
    {
        var queryProducts = from product in _catalogContext.Products
                            join cp in _catalogContext.CompanyProducts
                                on product.Id equals cp.ProductId
                            join company in _catalogContext.Companies
                                on cp.CompanyId equals company.Id
                            select new QueryProductCompany
                            {
                                CompanyCreateAt = company.CreateAt,
                                CompanyId = company.Id,
                                CompanyName = company.Name,
                                ProductDescription = product.Description,
                                ProductId = product.Id,
                                ProductName = product.Name,
                                ProductInsertAt = product.InsertAt,
                                ProductValue = product.Value
                            };

        await Task.CompletedTask;

        return Result.Success(queryProducts);
    }
}
