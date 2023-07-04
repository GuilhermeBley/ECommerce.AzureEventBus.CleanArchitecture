using Ecommerce.Catalog.Core.Exceptions;
using ECommerce.Catalog.Core.Primitive.Result;

namespace Ecommerce.Catalog.Core.Extension;

public static class ResultBuilderExtension
{
    public static void AddIfIsInRange<T>(this ResultBuilder<T> resultBuilder,
        string? str,
        ErrorEnum @enum,
        int minValue = 0,
        int maxValue = int.MaxValue,
        bool checkNull = true)
        => resultBuilder.AddIfIsInRange(
            str, 
            @enum.ToString(), 
            code: (int)@enum, 
            minValue: minValue, 
            maxValue: maxValue, 
            checkNull: checkNull);

    public static void AddIfNullOrWhiteSpace<T>(this ResultBuilder<T> resultBuilder, string? str, ErrorEnum @enum)
        => resultBuilder.AddIfNullOrWhiteSpace(str, @enum.ToString(), (int)@enum);

    public static void AddIfNullOrEmpty<T>(this ResultBuilder<T> resultBuilder, string? str, ErrorEnum @enum)
        => resultBuilder.AddIfNullOrEmpty(str, @enum.ToString(), (int)@enum);

    public static void AddIfNull<T>(this ResultBuilder<T> resultBuilder, object? obj, ErrorEnum @enum)
        => resultBuilder.AddIfNull(obj, @enum.ToString(), (int)@enum);

    public static void AddIf<T>(this ResultBuilder<T> resultBuilder, bool condition, ErrorEnum @enum)
        => resultBuilder.AddIf(condition, @enum.ToString(), (int)@enum);
}
