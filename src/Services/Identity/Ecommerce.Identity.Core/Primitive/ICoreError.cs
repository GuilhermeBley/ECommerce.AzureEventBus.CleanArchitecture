namespace Ecommerce.Identity.Core.Primitive;

public interface ICoreError
{
    int Code { get; }
    string Message { get; }
}