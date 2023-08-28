using Ecommerce.Catalog.Application.Model.Product;
using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Application.Security;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Catalog.Application.Commands.Product.GetProductById;

public class GetProductByIdQueryHandler
    : IAppRequestHandler<GetProductByIdRequest, Result<ProductModel?>>
{
    private readonly CatalogContext _catalogContext;
    private readonly IClaimProvider _claimProvider;

    public GetProductByIdQueryHandler(IClaimProvider principal, CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
        _claimProvider = principal;
    }

    public async Task<Result<ProductModel?>> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var productFound = await _catalogContext
            .Products
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        return ResultBuilder<ProductModel?>.CreateSuccess(productFound);
    }
}
