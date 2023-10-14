namespace Ecommerce.Identity.Application.Commands.Authentication.ResendEmailConfirmation;

public class ResendEmailConfirmationResponse
{
    public Guid EmailSentId { get; set; }
    public bool EmailSent { get; set; }
}
