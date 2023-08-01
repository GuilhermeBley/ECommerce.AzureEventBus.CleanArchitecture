using Ecommerce.Catalog.Application.Mediator;
using MediatR;

namespace Ecommerce.Catalog.Infrastructure.Mediator;

internal class RequestHandlerAdapter<TRequest, TResponse> 
    : IRequestHandler<RequestAdapter<TRequest, TResponse>, TResponse>
{
    private readonly IAppRequestHandler<TRequest, TResponse> myImpl;

    public RequestHandlerAdapter(IAppRequestHandler<TRequest, TResponse> impl)
    {
        myImpl = impl ?? throw new ArgumentNullException(nameof(impl));
    }

    public Task<TResponse> Handle(RequestAdapter<TRequest, TResponse> request, CancellationToken cancellationToken)
    {
        return myImpl.Handle(request.Value, cancellationToken);
    }
}
