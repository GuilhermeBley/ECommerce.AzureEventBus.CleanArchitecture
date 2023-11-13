using static Ecommerce.Identity.Core.Primitive.Result.ResultBuilder;

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

    public static Result Failed(ErrorEnum error)
        => new(new ICoreError[] { ParseErrorEnum(error) });

    public static Result Failed(params ErrorEnum[] errors)
        => new(errors.Select(e => ParseErrorEnum(e)));

    public static Result Success()
        => new(Array.Empty<ICoreError>());
    public static Result<T> Success<T>()
        => Success<T>(default);
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

    protected static internal ICoreError ParseErrorEnum(ErrorEnum @enum)
        => new InternalCoreErrorParser(@enum);

    private class InternalCoreErrorParser : ICoreError
    {
        public int Code { get; }

        public string Message { get; }

        public InternalCoreErrorParser(ErrorEnum @enum)
        {
            Code = (int)@enum;
            Message = @enum.ToString();
        }
    }
}