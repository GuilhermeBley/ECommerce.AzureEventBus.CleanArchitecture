using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Infrastructure.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Identity.Infrastructure.Extension.Di;

public static class DependencyInjection
{
    public static IServiceCollection AddMySqlContext(this IServiceCollection services)
    {
        services.AddDbContext<Context.MySqlDbContext>();

        return services;
    }

    public static IServiceCollection AddApplicationMediator(this IServiceCollection serviceDescriptors)
        => serviceDescriptors
        .AddMediatR(options => options.RegisterServicesFromAssemblies(typeof(IAppMediator).Assembly))
        .AddSingleton<IAppMediator, MediatRAdapter>()
        .AddTransient(typeof(IRequestHandler<,>), typeof(RequestHandlerAdapter<,>))
        .AddTransient(typeof(INotificationHandler<>), typeof(NotificationHandlerAdapter<>));
}
