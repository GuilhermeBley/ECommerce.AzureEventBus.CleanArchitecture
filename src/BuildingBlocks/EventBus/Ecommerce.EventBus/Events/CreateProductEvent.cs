namespace Ecommerce.EventBus.Events;

public class CreateProductEvent : IntegrationEvent
{
    public Guid Id { get; set; }
}