namespace Ecommerce.EventBus.Events;

public class UpdateProductEvent : IntegrationEvent
{
    public Guid Id { get; set; }
}