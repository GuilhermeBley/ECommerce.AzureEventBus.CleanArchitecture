using MediatR;

namespace Ecommerce.Catalog.Infrastructure.Mediator;

internal class RequestAdapter<TRequest, TResponse> : IRequest<TResponse>
{
    public RequestAdapter(TRequest value)
    {
        Value = value;
    }

    public TRequest Value { get; }
}

}
