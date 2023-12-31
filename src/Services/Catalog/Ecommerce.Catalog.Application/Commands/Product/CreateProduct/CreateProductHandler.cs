﻿using Ecommerce.Catalog.Application.Model.Company;
using Ecommerce.Catalog.Application.Model.Product;
using Ecommerce.Catalog.Application.Notifications.Product.CreateProduct;
using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Application.Security;
using Ecommerce.EventBus.Events;
using System.Security.Claims;

namespace Ecommerce.Catalog.Application.Commands.Product.CreateProduct;

public class CreateProductHandler : IAppRequestHandler<CreateProductRequest, Result<CreateProductResponse>>
{
    private readonly IEventBus _eventBus;
    private readonly CatalogContext _catalogContext;
    private readonly IClaimProvider _claimProvider;

    public CreateProductHandler(IClaimProvider principal, CatalogContext catalogContext, IEventBus eventBus)
    {
        _catalogContext = catalogContext;
        _eventBus = eventBus;
        _claimProvider = principal;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var principal = await _claimProvider.GetCurrentAsync() 
            ?? throw new InvalidOperationException();

        var claimCompany = principal.GetCompany();

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

        await _eventBus.PublishAsync(new CreateProductEvent { Id = productModel.Id });

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
