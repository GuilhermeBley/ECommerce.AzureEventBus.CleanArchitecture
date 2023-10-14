namespace Ecommerce.EventBus.Events;

public class CompanyUpdatedEvent : IntegrationEvent
{

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime UpdateAt { get; set; }
    public DateTime CreateAt { get; set; }
}
