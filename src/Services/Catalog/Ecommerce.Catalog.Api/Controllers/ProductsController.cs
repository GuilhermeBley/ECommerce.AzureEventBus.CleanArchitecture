using Ecommerce.Catalog.Api.Model.Product;
using Ecommerce.Catalog.Application.Commands.Product.CreateProduct;
using Ecommerce.Catalog.Application.Mediator;
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
}
