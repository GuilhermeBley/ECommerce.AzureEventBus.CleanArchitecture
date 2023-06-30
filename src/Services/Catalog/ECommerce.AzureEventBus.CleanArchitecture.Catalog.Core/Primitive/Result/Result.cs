using System.Diagnostics.CodeAnalysis;

namespace ECommerce.AzureEventBus.CleanArchitecture.Catalog.Core.Primitive.Result;

public class Result<TResult> : ResultBase
{
    private readonly TResult? _value;
    public TResult? Value => _value;

    protected Result(TResult? value, IEnumerable<ICoreError> errors) : base(errors)
    {
        _value = value;
    }

    public static Result<TResult> Success(TResult value)
        => new(value, Enumerable.Empty<ICoreError>());

    public static Result<TResult> Failed(ICoreError error)
        => new(default, new ICoreError[] { error });
        
    public static Result<TResult> Failed(IEnumerable<ICoreError> errors)
        => new(default, errors);

    public bool TryGetValue([NotNullWhen(true)] out TResult? value)
    {
        value = default;

        if (_value is null)
            return false;

        value = _value;
        return true;
    }
}