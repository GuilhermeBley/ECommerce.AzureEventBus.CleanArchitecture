using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Catalog.Infrastructure.Extensions.Di;

internal static class InternalDiExtensions
{
    public static IServiceCollection RegisterAsImplementedInterfaces(
        this IServiceCollection serviceCollection,
        Assembly assemblyToMap,
        IEnumerable<Type> interfaces,
        ServiceLifetime lifeTime = ServiceLifetime.Scoped)
    {
        foreach (var typeToImplement in interfaces)
        {
            var classToImplement =
                assemblyToMap
                .GetTypes()
                .Where(classType => classType.IsPublic)
                .Where(classType => classType.IsClass)
                .Where(classType => !classType.IsAbstract)
                .Where(classType => IsAssignableFromCheckingGenerics(classType, typeToImplement))
                .ToArray();

            foreach (var classToImp in classToImplement)
                serviceCollection.Add(
                    new (serviceType: classToImp, implementationType: classToImp, lifetime: lifeTime));
        }

        return serviceCollection;
    }

    private static bool IsAssignableFromCheckingGenerics(
        Type classType,
        Type interfaceType)
    {
        if (interfaceType.IsAssignableFrom(classType))
            return true;

        foreach (Type classInterfaces in classType.GetInterfaces())
        {
            if (classInterfaces.IsGenericType && classInterfaces.GetGenericTypeDefinition() == interfaceType)
                return true;
        }

        return false;
    }
}