using System.Diagnostics.CodeAnalysis;

namespace ECommerce.Catalog.Core.Primitive.Result;

public sealed class ResultBuilder<TResult>
{
    public const int DEFAULT_CODE = 400;

    private readonly List<ICoreError> _errors = new();
    public bool HasError => _errors.Any();

    public ResultBuilder()
    {
    }

    public ResultBuilder(ResultBuilder<TResult> resultBuilder)
    {
        _errors.AddRange(resultBuilder._errors);
    }

    public ResultBuilder(IEnumerable<ICoreError> errors)
    {
        _errors.AddRange(errors);
    }

    public void AddIfLength(string? str, string message, int code = DEFAULT_CODE, int minValue = 0, int maxValue = int.MaxValue, bool addIfNull = true)
    {
        if (minValue < 0)
            throw new ArgumentOutOfRangeException("minValue");

        if (str is null && !addIfNull)
            return;

        if (str is null)
        {
            Add(message, code);
            return;
        }

        if (str.Length < minValue || str.Length > maxValue)
            Add(message, code);
    }

    public void AddIfNullOrWhiteSpace(string? str, string message, int code = DEFAULT_CODE)
        => AddIf(string.IsNullOrWhiteSpace(str), message, code);

    public void AddIfNullOrEmpty(string? str, string message, int code = DEFAULT_CODE)
        => AddIf(string.IsNullOrEmpty(str), message, code);

    public void AddIfNull(object? obj, string message, int code = DEFAULT_CODE)
        => AddIf(obj is null, message, code);

    public void AddIf(bool condition, string message, int code = DEFAULT_CODE)
    {
        if (condition)
            Add(message, code);
    }

    public void Add(string message, int code = DEFAULT_CODE)
        => _errors.Add(new Error(code, message));

    public void AddRange(params (string Message, int Code)[] values)
        => _errors.AddRange(values.Select(value => new Error(value.Code, value.Message)));

    public Result<TResult> Failed()
    {
        if (!HasError)
            throw new InvalidOperationException("There aren't errors.");

        return Result<TResult>.Failed(_errors.AsReadOnly());
    }

    public Result<TResult> Success(TResult result)
    {
        if (result is null)
            throw new ArgumentNullException("result");

        if (HasError)
            throw new InvalidOperationException("There aren't errors.");

        return Result<TResult>.Success(result);
    }

    public bool TrySuccess(TResult result, [NotNullWhen(true)] out Result<TResult>? resultValue)
    {
        resultValue = null;

        if (HasError)
            return false;

        if (result is null)
            return false;

        resultValue = Result<TResult>.Success(result);
        return true;
    }

    public bool TryFailed([NotNullWhen(true)] out Result<TResult>? resultValue)
    {
        resultValue = null;

        if (!HasError)
            return false;

        resultValue = Result<TResult>.Failed(_errors.AsReadOnly());
        return true;
    }

    public static Result<TResult> CreateFailed(string message, int code = DEFAULT_CODE)
        => Result<TResult>.Failed(new Error(code, message));

    public static Result<TResult> CreateSuccess(TResult result)
        => Result<TResult>.Success(result ?? throw new ArgumentNullException("result"));

    private record Error(int Code, string Message) : ICoreError;
}