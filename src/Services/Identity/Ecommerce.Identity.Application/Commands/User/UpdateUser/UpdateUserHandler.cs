using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Application.Security;
using Ecommerce.Identity.Application.Security.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.User.UpdateUser;

public class UpdateUserHandler : IAppRequestHandler<UpdateUserRequest, Result<UpdateUserResponse>>
{
    private readonly IdentityContext _context;
    private readonly IClaimProvider _claimProvider;

    public UpdateUserHandler(
        IdentityContext context,
        Security.IClaimProvider claimProvider)
    {
        _context = context;
        _claimProvider = claimProvider;
    }

    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var resultBuilder = new ResultBuilder<UpdateUserResponse>();

        var user = await _claimProvider.GetCurrentAsync();

        if (user is null)
            return ResultBuilderExtension.CreateFailed<UpdateUserResponse>(ErrorEnum.Unauthorized);

        var resultUser = user.GetUserId();

        if (!resultUser.TryGetValue(out Guid userId))
        {
            return ResultBuilderExtension.CreateFailed<UpdateUserResponse>(ErrorEnum.Unauthorized);
        }

        var userFound = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.IdToUpdate);

        if (userFound is null)
            return ResultBuilderExtension.CreateFailed<UpdateUserResponse>(ErrorEnum.UserNotFound);

        var userEntityResult = Ecommerce.Identity.Core.Entities.User.CreateWithHashedPassword(
            id: userId,
            email: request.Email,
            name: request.Name,
            nickName: request.NickName,
            passwordHash: userFound.PasswordHash,
            passwordSalt: userFound.PasswordSalt,
            phoneNumber: request.PhoneNumber,
            twoFactoryEnabled: userFound.TwoFactoryEnabled,
            lockOutEnabled: userFound.LockOutEnabled,
            phoneNumberConfirmed: userFound.PhoneNumberConfirmed
        );

        resultBuilder.AddRange(userEntityResult.Errors);

        if (!userEntityResult.TryGetValue(out var userEntityValidated))
            return resultBuilder.Failed();

        userFound.NickName = userEntityValidated.NickName;
        userFound.PhoneNumber = userEntityValidated.PhoneNumber;
        userFound.Email = userEntityValidated.Email;
        userFound.Name = userEntityValidated.Name;

        await _context.SaveChangesAsync();

        return resultBuilder.Success(
            new UpdateUserResponse
            {
                Email = userFound.Email, Name = userFound.Name, EmailConfirmed = userFound.EmailConfirmed, Id = userId,
                NickName = userFound.NickName, PhoneNumber = userFound.PhoneNumber
            });
    }
}
