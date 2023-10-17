using Ecommerce.EventBus.Abstractions;
using Ecommerce.EventBus.Events;
using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.Authentication.ResendEmailConfirmation;

public class ResendEmailConfirmationHandler
    : IAppRequestHandler<ResendEmailConfirmationRequest, Result<ResendEmailConfirmationResponse>>
{
    private readonly IEventBus _eventBus;
    private readonly IdentityContext _identityContext;

    public ResendEmailConfirmationHandler(
        IEventBus eventBus,
        IdentityContext identityContext)
    {
        _eventBus = eventBus;
        _identityContext = identityContext;
    }

    public async Task<Result<ResendEmailConfirmationResponse>> Handle(ResendEmailConfirmationRequest request, CancellationToken cancellationToken)
    {
        var userFound = await _identityContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        var result = new ResendEmailConfirmationResponse
        {
            EmailSentId = Guid.NewGuid(),
        };

        if (userFound is null)
        {
            result.EmailSent = false;
            return Result.Success(result);
        }
        
        result.EmailSent = true;

        await _eventBus.PublishAsync(new ResendConfirmationUserCreatedEvent
        {
            EmailSentId = result.EmailSentId
        });

        return Result.Success(result);
    }
}
