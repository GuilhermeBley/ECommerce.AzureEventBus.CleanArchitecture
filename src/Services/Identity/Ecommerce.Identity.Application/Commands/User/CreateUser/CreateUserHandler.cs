using Ecommerce.EventBus.Abstractions;
using Ecommerce.EventBus.Events;
using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.User.CreateUser;

public class CreateUserHandler : IAppRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
{
    private readonly IEventBus _eventBus;
    private readonly IdentityContext _identityContext;

    public CreateUserHandler(
        IEventBus eventBus,
        IdentityContext identityContext)
    {
        _eventBus = eventBus;
        _identityContext = identityContext;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var resultUser = Core.Entities.User.Create(
            id: Guid.NewGuid(), email: request.Email, name: request.Name,
            nickName: request.NickName, password: request.Password,
            phoneNumber: request.PhoneNumber
        );

        if (!resultUser.TryGetValue(out var user))
            return Result<CreateUserResponse>.Failed(resultUser);

        await using var transaction = await _identityContext.Database.BeginTransactionAsync(cancellationToken);

        var userByEmail = await _identityContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email.ToUpperInvariant());

        if (userByEmail is not null)
            return ResultBuilderExtension.CreateFailed<CreateUserResponse>(ErrorEnum.ConflicUser);

        var userEntry = await _identityContext.Users.AddAsync(new Model.UserModel
        {
            AccessFailedCount = user.AccessFailedCount,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            Id = user.Id,
            LockOutEnabled = user.LockOutEnabled,
            LockOutEnd = user.LockOutEnd,
            Name = user.Name,
            NickName = user.NickName,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactoryEnabled = user.TwoFactoryEnabled,
        }, cancellationToken);

        await transaction.CommitAsync();
        await _identityContext.SaveChangesAsync();

        await _eventBus.PublishAsync(new CreateUserEvent
        {
            Email = user.Email,
            Id = user.Id,
            Name = user.Name,
            NickName = user.NickName
        });

        return Result.Success(new CreateUserResponse
        {
            AccessFailedCount = userEntry.Entity.AccessFailedCount,
            TwoFactoryEnabled = userEntry.Entity.TwoFactoryEnabled,
            Email = userEntry.Entity.Email,
            EmailConfirmed = userEntry.Entity.EmailConfirmed,
            Id = userEntry.Entity.Id,
            LockOutEnabled = userEntry.Entity.LockOutEnabled,
            LockOutEnd= userEntry.Entity.LockOutEnd, 
            Name = userEntry.Entity.Name,
            PhoneNumber = userEntry.Entity.PhoneNumber,
            NickName = userEntry.Entity.NickName,
            PhoneNumberConfirmed = userEntry.Entity.PhoneNumberConfirmed
        });
    }
}
