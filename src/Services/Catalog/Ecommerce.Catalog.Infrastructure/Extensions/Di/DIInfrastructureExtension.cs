using Ecommerce.Catalog.Application.Mediator;
using Ecommerce.Catalog.Application.Repositories;
using Ecommerce.Catalog.Application.Security;
using Ecommerce.Catalog.Infrastructure.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Catalog.Infrastructure.Extensions.Di;

public static class DIInfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceDescriptors)
    {
        return serviceDescriptors
            .AddApplicationMediator()
            .AddApplicationContext();
    }

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
        .AddDbContext<PostgreSql.PostgreCatalogContext>()
        .AddScoped<CatalogContext>(provider => provider.GetRequiredService<PostgreSql.PostgreCatalogContext>());

}
