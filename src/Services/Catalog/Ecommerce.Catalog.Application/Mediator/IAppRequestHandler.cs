namespace Ecommerce.Catalog.Application.Mediator;

public interface 

public interface IAppRequestHandler<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
