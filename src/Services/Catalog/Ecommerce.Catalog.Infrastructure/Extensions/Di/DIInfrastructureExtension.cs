using Ecommerce.Catalog.Application.Mediator;
using Ecommerce.Catalog.Infrastructure.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Catalog.Infrastructure.Extensions.Di;

public static class DIInfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceDescriptors)
    {
        return serviceDescriptors
            .AddApplicationMediator();
    }

    public static IServiceCollection AddApplicationMediator(this IServiceCollection serviceDescriptors)
        => serviceDescriptors
        .AddMediatR(options => options.RegisterServicesFromAssemblies(typeof(IAppMediator).Assembly))
        .AddSingleton<IAppMediator, MediatRAdapter>()
        .AddTransient(typeof(IRequestHandler<,>), typeof(RequestHandlerAdapter<,>))
        .AddTransient(typeof(INotificationHandler<>), typeof(NotificationHandlerAdapter<>));

}
