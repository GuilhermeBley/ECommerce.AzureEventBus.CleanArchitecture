using System.Diagnostics.CodeAnalysis;

namespace ECommerce.Catalog.Core.Primitive.Result;

public sealed class ResultBuilder<TResult> : ResultBuilder
{
    public ResultBuilder()
    {
    }

    public ResultBuilder(ResultBuilder resultBuilder)
        : base(resultBuilder)
    {
    }

    public ResultBuilder(ResultBase resultBase)
        : base(resultBase.Errors)
    {
    }

    public ResultBuilder(IEnumerable<ICoreError> errors)
        : base(errors)
    {
    }

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

    public static new Result<TResult> CreateFailed(string message, int code = DEFAULT_CODE)
        => Result<TResult>.Failed(new Error(code, message));

    public static Result<TResult> CreateSuccess(TResult result)
        => Result<TResult>.Success(result ?? throw new ArgumentNullException("result"));
}

public class ResultBuilder
{
    public const int DEFAULT_CODE = 400;

    internal protected readonly List<ICoreError> _errors = new();
    public bool HasError => _errors.Any();

    public ResultBuilder()
    {
    }

    public ResultBuilder(ResultBuilder resultBuilder)
    {
        _errors.AddRange(resultBuilder._errors);
    }

    public ResultBuilder(IEnumerable<ICoreError> errors)
    {
        _errors.AddRange(errors);
    }

    public void AddIfIsInRange(
        string? str,
        string message,
        int minValue = 0,
        int maxValue = int.MaxValue,
        bool checkNull = true,
        int code = DEFAULT_CODE)
    {
        if (checkNull && str is null)
        {
            Add(message: message, code: code);
            return;
        }

        if (str is null)
            return;

        if (str.Length < minValue || str.Length > maxValue)
        {
            Add(message: message, code: code);
            return;
        }
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

    public ResultBase GetResult()
        => new ResultBase(_errors);

    public static ResultBase CreateFailed(string message, int code = DEFAULT_CODE)
        => new ResultBase(new ICoreError[] { new Error(code, message) });

    public static ResultBase CreateSuccess()
        => new ResultBase(Enumerable.Empty<ICoreError>());

    internal protected record Error(int Code, string Message) : ICoreError;
}