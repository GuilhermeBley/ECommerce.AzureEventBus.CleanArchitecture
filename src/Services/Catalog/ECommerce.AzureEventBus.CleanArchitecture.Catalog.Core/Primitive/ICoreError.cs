namespace ECommerce.AzureEventBus.CleanArchitecture.Catalog.Core.Primitive;

public interface ICoreError
{
    int Code { get; }
    string Message { get; }
}