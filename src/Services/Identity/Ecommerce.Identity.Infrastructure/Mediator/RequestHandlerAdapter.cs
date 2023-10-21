using Ecommerce.Identity.Application.Mediator;
using MediatR;
using System.Diagnostics;

namespace Ecommerce.Identity.Infrastructure.Mediator;

[DebuggerNonUserCode]
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
