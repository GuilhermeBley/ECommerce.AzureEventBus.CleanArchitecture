namespace Ecommerce.EventBus.Events;

/// <summary>
/// Event is invoked when e-mail confirmation is needed
/// </summary>
public class ResendConfirmationUserCreatedEvent : IntegrationEvent
{
    public Guid EmailSentId { get; set; }
}
