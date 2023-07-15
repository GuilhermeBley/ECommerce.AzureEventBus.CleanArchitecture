using Ecommerce.Catalog.Application.Repositories;

namespace Ecommerce.Catalog.Application.Commands.Product.CreateProduct;

public class CreateProductHandler : IAppRequestHandler<CreateProductRequest, Result<CreateProductResponse>>
{
    private readonly ICatalogContext _catalogContext;

    public CreateProductHandler(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public Task<Result<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        ResultBuilder resultBuilder = new();

        throw new NotImplementedException();
    }
}
