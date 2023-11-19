namespace Ecommerce.EventBus.Events;

public class CompanyDisabledEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Disabled { get; set; }
    public DateTime UpdateAt { get; set; }
    public DateTime CreateAt { get; set; }
}
