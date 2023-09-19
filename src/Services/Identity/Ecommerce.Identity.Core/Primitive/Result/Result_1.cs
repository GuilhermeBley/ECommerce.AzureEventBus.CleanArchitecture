namespace Ecommerce.Identity.Core.Primitive.Result;

public class Result
{
    /// <summary>
    /// Gets a value indicating whether the result is a success result.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the result is a failure result.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the error.
    /// </summary>
    public IReadOnlyCollection<ICoreError> Errors { get; }

    internal Result(IEnumerable<ICoreError> errors)
    {
        Errors = errors.ToList().AsReadOnly();
        IsSuccess = !Errors.Any();
    }

    public static Result Failed(ICoreError error)
        => new(new ICoreError[] { error });

    public static Result Failed(IEnumerable<ICoreError> errors)
        => new(errors);

    public static Result Success()
        => new(Array.Empty<ICoreError>());
    public static Result<T> Success<T>(T? item)
        => Result<T>.Success(item);

    /// <summary>
    /// Concat errors
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public Result ConcatErrors(Result other)
        => !this.Errors.Any() && !other.Errors.Any()
        ? throw new InvalidOperationException("This or other doesn't contain errors.")
        : new Result(this.Errors.Concat(other.Errors));
}