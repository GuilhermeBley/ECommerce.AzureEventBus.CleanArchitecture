using Ecommerce.Catalog.Application.Model.Identity;
using Ecommerce.Catalog.Application.Repositories;
using System.Security.Claims;
using System.Security.Principal;

namespace Ecommerce.Catalog.Application.Commands.Product.CreateProduct;

public class CreateProductHandler : IAppRequestHandler<CreateProductRequest, Result<CreateProductResponse>>
{
    private readonly ICatalogContext _catalogContext;
    private readonly ClaimsPrincipal _principal;

    public CreateProductHandler(ClaimsPrincipal principal, ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
        _principal = principal;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = Core.Entities.Product.Product.Create(
            id: Guid.NewGuid(),
            name: request.Name,
            description: request.Description,
            value: request.Value,
            updateAt: DateTime.UtcNow,
            insertAt: DateTime.UtcNow
        );

        if (result.IsFailure ||
            result.Value is null)
            return Result<CreateProductResponse>.Failed(result.Errors);

        var productModel = Map(result.Value);

        await _catalogContext.Products.AddAsync(productModel);

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
}
