namespace Ecommerce.EventBus.Events;

public class DisableProductEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
}
