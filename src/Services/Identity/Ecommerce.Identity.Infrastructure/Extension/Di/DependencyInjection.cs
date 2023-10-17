using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Identity.Infrastructure.Extension.Di;

public static class DependencyInjection
{
    public static IServiceCollection AddMySqlContext(this IServiceCollection services)
    {
        services.AddDbContext<Context.MySqlDbContext>();

        return services;
    }
}
