using Ecommerce.Catalog.Api.Model.Product;
using Ecommerce.Catalog.Application.Commands.Product.CreateProduct;
using Ecommerce.Catalog.Application.Commands.Product.GetProductById;
using Ecommerce.Catalog.Application.Commands.Product.GetProductsWithCompany;
using Ecommerce.Catalog.Application.Commands.Product.UpdateProduct;
using Ecommerce.Catalog.Application.Mediator;
using Ecommerce.Catalog.Application.Model.Product;
using Ecommerce.Catalog.Application.Notifications.Product.UpdateProduct;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IAppMediator _mediator;

    public ProductsController(IAppMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct(
        PostProductModel model, CancellationToken cancellationToken = default)
    {
        var request = new CreateProductRequest
        {
            Description = model.Description,
            Name = model.Name,
            Value = model.Value,
        };

        var result = await _mediator.Send<CreateProductRequest, Result<CreateProductResponse>>(request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductModel>> GetProduct(Guid productId, CancellationToken cancellationToken = default)
    {
        var request = new GetProductByIdRequest
        {
            Id = productId
        };

        var result = await _mediator.Send<GetProductByIdRequest, Result<ProductModel?>>(request, cancellationToken);

        if (result.TryGetValue(out var productModel))
        {
            if (productModel is null)
                return NoContent();

            return Ok(productModel);
        }

        return BadRequest(result.Errors);
    }

    [HttpGet()]
    public async Task<ActionResult<ProductModel>> GetProducts(CancellationToken cancellationToken = default)
    {
        var request = new GetProductWithCompanyRequest
        {
        };
        
        var result = await _mediator.Send<GetProductWithCompanyRequest, Result<IQueryable<QueryProductCompany>>>(request, cancellationToken);

        if (result.TryGetValue(out var productsModels))
        {
            if (productsModels is null)
                return NoContent();

            return Ok(productsModels);
        }

        return BadRequest(result.Errors);
    }

    [HttpPut]
    public async Task<ActionResult<ProductModel>> GetProducts([FromBody]UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send<UpdateProductRequest, Result<UpdateProductResponse>>(request, cancellationToken);

        if (result.TryGetValue(out var updateProductResponse))
        {
            return Ok(updateProductResponse);
        }

        return BadRequest(result.Errors);
    }
}
