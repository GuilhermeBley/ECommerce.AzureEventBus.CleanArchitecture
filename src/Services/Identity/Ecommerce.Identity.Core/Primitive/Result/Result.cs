using System.Diagnostics.CodeAnalysis;

namespace Ecommerce.Identity.Core.Primitive.Result;

public class Result<TResult> : Result
{
    private readonly TResult? _value;
    public TResult? Value => _value;

    protected Result(TResult? value, IEnumerable<ICoreError> errors) : base(errors)
    {
        _value = value;
    }

    public static Result<TResult> Success(TResult? value)
        => new(value, Enumerable.Empty<ICoreError>());

    public static Result<TResult> Failed(Result result)
        => Failed(result.Errors);

    public new static Result<TResult> Failed(ICoreError error)
        => new(default, new ICoreError[] { error });
        
    public new static Result<TResult> Failed(IEnumerable<ICoreError> errors)
        => new(default, errors);

    public bool TryGetValue([NotNullWhen(true)] out TResult? value)
    {
        value = default;

        if (_value is null)
            return false;

        value = _value;
        return true;
    }

    /// <summary>
    /// Concat errors
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public new Result<TResult> ConcatErrors(Result other)
        => !this.Errors.Any() && !other.Errors.Any()
        ? throw new InvalidOperationException("This or other doesn't contain errors.")
        : new Result<TResult>(default, this.Errors.Concat(other.Errors));
}