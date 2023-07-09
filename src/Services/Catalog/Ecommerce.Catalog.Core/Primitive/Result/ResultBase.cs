namespace Ecommerce.Catalog.Core.Primitive.Result;

public class ResultBase
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

    internal ResultBase(IEnumerable<ICoreError> errors)
    {
        Errors = errors.ToList().AsReadOnly();
        IsSuccess = Errors.Any();
    }

    /// <summary>
    /// Concat errors
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public ResultBase ConcatErrors(ResultBase other)
        => !this.Errors.Any() && !other.Errors.Any()
        ? throw new InvalidOperationException("This or other doesn't contain errors.")
        : new ResultBase(this.Errors.Concat(other.Errors));
}