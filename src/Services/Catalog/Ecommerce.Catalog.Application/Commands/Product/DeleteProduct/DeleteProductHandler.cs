﻿using Ecommerce.Catalog.Application.Commands.Product.CreateProduct;
using Ecommerce.Catalog.Application.Notifications.Product.DeleteProduct;
using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Application.Security;
using Ecommerce.Catalog.Core.Extension;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Catalog.Application.Commands.Product.DeleteProduct;

public class DeleteProductHandler : IAppRequestHandler<DeleteProductRequest, Result<DeleteProductResponse>>
{
    private readonly IEventBus _eventBus;
    private readonly CatalogContext _catalogContext;
    private readonly IClaimProvider _claimProvider;

    public DeleteProductHandler(IClaimProvider principal, CatalogContext catalogContext, IEventBus eventBus)
    {
        _eventBus = eventBus;
        _catalogContext = catalogContext;
        _claimProvider = principal;
    }

    public async Task<Result<DeleteProductResponse>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var principal = await _claimProvider.GetCurrentAsync()
            ?? throw new InvalidOperationException();

        var claimCompany = principal.GetCompany();

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

        await _eventBus.PublishAsync(new DeleteProductNotification
        { 
            Id = product.Id,
            Name = product.Name,
            Value = product.Value,
        });

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
