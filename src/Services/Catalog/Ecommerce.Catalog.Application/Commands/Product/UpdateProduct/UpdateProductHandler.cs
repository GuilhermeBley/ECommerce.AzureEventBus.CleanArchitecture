using Ecommerce.Catalog.Application.Commands.Product.CreateProduct;
using Ecommerce.Catalog.Application.Commands.Product.DeleteProduct;
using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Core.Entities.Product;
using Ecommerce.Catalog.Core.Extension;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Catalog.Application.Commands.Product.UpdateProduct;

public class UpdateProductHandler : IAppRequestHandler<UpdateProductRequest, Result<UpdateProductResponse>>
{
    private readonly CatalogContext _catalogContext;
    private readonly ClaimsPrincipal _principal;

    public UpdateProductHandler(ClaimsPrincipal principal, CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
        _principal = principal;
    }

    public async Task<Result<UpdateProductResponse>> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var claimCompany = _principal.GetCompany();

        if (!claimCompany.TryGetValue(out Guid companyId))
            return Result<UpdateProductResponse>.Failed(claimCompany.Errors);

        using var transaction = await _catalogContext.Database.BeginTransactionAsync();

        var productToUpdate = await
            (from companyPro in _catalogContext.CompanyProducts
            join prod in _catalogContext.Products
                on companyPro.ProductId equals prod.Id
            where companyPro.CompanyId == companyId
            select prod)
            .FirstOrDefaultAsync(c => c.Id == request.ProductId);

        if (productToUpdate is null)
            return ResultBuilderExtension
                .CreateFailed<UpdateProductResponse>(Core.Exceptions.ErrorEnum.ProductNotFound);

        var resultEntityProduct = Core.Entities.Product.Product.Create(
            id: productToUpdate.Id,
            name: request.NewName,
            description: request.NewDescription,
            value: request.NewValue,
            updateAt: DateTime.UtcNow,
            insertAt: productToUpdate.InsertAt
        );

        if (resultEntityProduct.IsFailure ||
            resultEntityProduct.Value is null)
            return Result<UpdateProductResponse>.Failed(resultEntityProduct.Errors);

        productToUpdate.Value = resultEntityProduct.Value.Value;
        productToUpdate.UpdateAt = resultEntityProduct.Value.UpdateAt;
        productToUpdate.Description = resultEntityProduct.Value.Description;
        productToUpdate.Name = resultEntityProduct.Value.Name;

        await transaction.CommitAsync();
        await _catalogContext.SaveChangesAsync();

        return Result<UpdateProductResponse>.Success(
                new UpdateProductResponse
                {
                    Description = productToUpdate.Description,
                    Name = productToUpdate.Name,
                    InsertAt = productToUpdate.InsertAt,
                    UpdateAt = productToUpdate.UpdateAt,
                    Value = productToUpdate.Value
                }
            );
    }
}
