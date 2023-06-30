namespace ECommerce.AzureEventBus.CleanArchitecture.Catalog.Core.Primitive.Result;

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

    protected ResultBase(IEnumerable<ICoreError> errors)
    {
        Errors = errors.ToList().AsReadOnly();
        IsSuccess = Errors.Any();
    }
}