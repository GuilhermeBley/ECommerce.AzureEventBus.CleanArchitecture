using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Security;
using Ecommerce.Identity.Infrastructure.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Identity.Infrastructure.Extension.Di;

public static class DependencyInjection
{
    public static IServiceCollection AddMySqlContext(this IServiceCollection services)
        => services
            .AddApplicationContext()
            .AddApplicationMediator();

    public static IServiceCollection AddClaimProvider(this IServiceCollection serviceDescriptors, Func<IServiceProvider, IClaimProvider> factory)
        => serviceDescriptors
        .AddScoped(factory);

    public static IServiceCollection AddApplicationMediator(this IServiceCollection serviceDescriptors)
        => serviceDescriptors
        .AddMediatR(options => options.RegisterServicesFromAssemblies(typeof(IAppMediator).Assembly))
        .AddSingleton<IAppMediator, MediatRAdapter>()
        .AddTransient(typeof(IRequestHandler<,>), typeof(RequestHandlerAdapter<,>))
        .AddTransient(typeof(INotificationHandler<>), typeof(NotificationHandlerAdapter<>));

    private static IServiceCollection AddApplicationContext(this IServiceCollection serviceDescriptors)
        => serviceDescriptors
        .AddDbContext<Context.MySqlDbContext>()
        .AddScoped<Application.Repositories.IdentityContext>(provider => provider.GetRequiredService<Context.MySqlDbContext>());
}
