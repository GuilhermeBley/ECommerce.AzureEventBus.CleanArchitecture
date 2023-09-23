﻿namespace Ecommerce.Identity.Application.Mediator;

public interface IAppRequestHandler<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
