using MediatR;
using System.Diagnostics;

namespace Ecommerce.Catalog.Infrastructure.Mediator;

[DebuggerNonUserCode]
internal class RequestAdapter<TRequest, TResponse> : IRequest<TResponse>
{
    public RequestAdapter(TRequest value)
    {
        Value = value;
    }

    public TRequest Value { get; }
}
