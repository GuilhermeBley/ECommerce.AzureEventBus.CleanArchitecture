using Ecommerce.Catalog.Application.Model.Company;
using Ecommerce.Catalog.Application.Model.Product;
using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Core.Entities.Company;
using System.Security.Claims;
using System.Security.Principal;

namespace Ecommerce.Catalog.Application.Commands.Product.CreateProduct;

public class CreateProductHandler : IAppRequestHandler<CreateProductRequest, Result<CreateProductResponse>>
{
    private readonly CatalogContext _catalogContext;
    private readonly ClaimsPrincipal _principal;

    public CreateProductHandler(ClaimsPrincipal principal, CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
        _principal = principal;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var claimCompany = _principal.GetCompany();

        if (!claimCompany.TryGetValue(out Guid companyId))
            return Result<CreateProductResponse>.Failed(claimCompany.Errors);

        using var transaction = await _catalogContext.Database.BeginTransactionAsync(cancellationToken);

        var resultProduct = Core.Entities.Product.Product.Create(
            id: Guid.NewGuid(),
            name: request.Name,
            description: request.Description,
            value: request.Value,
            updateAt: DateTime.UtcNow,
            insertAt: DateTime.UtcNow
        );

        if (resultProduct.IsFailure ||
            resultProduct.Value is null)
            return Result<CreateProductResponse>.Failed(resultProduct.Errors);

        var productModel = Map(resultProduct.Value);
        await _catalogContext.Products.AddAsync(productModel);

        var resultCompany = Core.Entities.Company.CompanyProduct.CreateDefault(
            productId: productModel.Id,
            companyId: companyId
            );

        if (resultCompany.IsFailure ||
            resultCompany.Value is null)
            return Result<CreateProductResponse>.Failed(resultCompany.Errors);

        var companyModel = Map(resultCompany.Value);
        await _catalogContext.CompanyProducts.AddAsync(companyModel);

        await _catalogContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return Result<CreateProductResponse>.Success(
            new CreateProductResponse
            {
                Description = productModel.Description,
                Id = productModel.Id,
                InsertAt = productModel.InsertAt,
                Name = productModel.Name,
                UpdateAt = productModel.UpdateAt,
                Value = productModel.Value
            }    
        );
    }

    public ProductModel Map(Core.Entities.Product.Product entity)
        => new ProductModel
        {
            Description = entity.Description,
            Name = entity.Name,
            Id = entity.Id,
            InsertAt = entity.InsertAt,
            UpdateAt = entity.UpdateAt,
            Value = entity.Value
        };

    public CompanyProductModel Map(Core.Entities.Company.CompanyProduct createProductResponse)
        => new CompanyProductModel
        {
            CompanyId = createProductResponse.CompanyId,
            CreateAt = createProductResponse.CreateAt,
            Id = createProductResponse.Id,
            ProductId = createProductResponse.ProductId
        };
}
