namespace Ecommerce.EventBus.Events;

public class DeleteProductEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
}
