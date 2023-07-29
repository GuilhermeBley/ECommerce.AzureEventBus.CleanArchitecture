namespace Ecommerce.Catalog.Core.Extension;

public static class ResultBuilderExtension
{
    public static void AddIfIsInRange(this ResultBuilder resultBuilder,
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

    public static void AddIfNullOrWhiteSpace(this ResultBuilder resultBuilder, string? str, ErrorEnum @enum)
        => resultBuilder.AddIfNullOrWhiteSpace(str, @enum.ToString(), (int)@enum);

    public static void AddIfNullOrEmpty(this ResultBuilder resultBuilder, string? str, ErrorEnum @enum)
        => resultBuilder.AddIfNullOrEmpty(str, @enum.ToString(), (int)@enum);

    public static void AddIfNull(this ResultBuilder resultBuilder, object? obj, ErrorEnum @enum)
        => resultBuilder.AddIfNull(obj, @enum.ToString(), (int)@enum);

    public static void AddIf(this ResultBuilder resultBuilder, bool condition, ErrorEnum @enum)
        => resultBuilder.AddIf(condition, @enum.ToString(), (int)@enum);

    public static Result CreateFailed(ErrorEnum @enum)
        => ResultBuilder.CreateFailed(@enum.ToString(), (int)@enum);

    public static Result<T> CreateFailed<T>(ErrorEnum @enum)
        => ResultBuilder<T>.CreateFailed(@enum.ToString(), (int)@enum);
}
