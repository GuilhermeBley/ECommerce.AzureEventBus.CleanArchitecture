using Ecommerce.Catalog.Application.Commands.Product.CreateProduct;
using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Core.Extension;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Catalog.Application.Commands.Product.DeleteProduct;

public class DeleteProductHandler : IAppRequestHandler<DeleteProductRequest, Result<DeleteProductResponse>>
{
    private readonly CatalogContext _catalogContext;
    private readonly ClaimsPrincipal _principal;

    public DeleteProductHandler(ClaimsPrincipal principal, CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
        _principal = principal;
    }

    public async Task<Result<DeleteProductResponse>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var claimCompany = _principal.GetCompany();

        if (!claimCompany.TryGetValue(out Guid companyId))
            return Result<DeleteProductResponse>.Failed(claimCompany.Errors);

        using var transaction = await _catalogContext.Database.BeginTransactionAsync();

        var productCompany = await _catalogContext
            .CompanyProducts
            .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.ProductId == request.ProductId);

        var product = await _catalogContext
            .Products
            .FirstOrDefaultAsync(c => c.Id == request.ProductId);

        if (productCompany is null ||
            product is null)
            return ResultBuilderExtension
                .CreateFailed<DeleteProductResponse>(Core.Exceptions.ErrorEnum.ProductNotFound);

        _catalogContext.Products.Remove(product);

        await transaction.CommitAsync();
        await _catalogContext.SaveChangesAsync();

        return Result<DeleteProductResponse>.Success(
            new DeleteProductResponse
            {
                Id = product.Id,
                Description = product.Description,
                InsertAt = product.InsertAt,
                Name = product.Name,
                UpdateAt = product.UpdateAt,
                Value = product.Value
            }    
        );
    }
}
