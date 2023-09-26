using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.Authentication.LoginUser;

public class LoginUserHandler
    : IAppRequestHandler<LoginUserRequest, Result<LoginUserResponse>>
{
    private readonly IdentityContext _identityContext;

    public LoginUserHandler(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }

    public async Task<Result<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var unauthorizedResult = ResultBuilderExtension.CreateFailed<LoginUserResponse>(ErrorEnum.Unauthorized);

        if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password)) 
        {
            return unauthorizedResult;
        }

        var userFound = await _identityContext.Users.FirstOrDefaultAsync(u => u.Email == request.Login);

        if (userFound is null)
            return unauthorizedResult;

        if (userFound.LockOutEnabled && userFound.LockOutEnd <= DateTime.UtcNow)
            return unauthorizedResult;

        if (!userFound.EmailConfirmed)
            return unauthorizedResult;

        if (userFound.AccessFailedCount > 10)
            return unauthorizedResult;

        if (Core.Entities.User.IsValidEncryption(request.Password, userFound.PasswordHash, userFound.PasswordSalt).IsFailure)
            return unauthorizedResult;

        return Result<LoginUserResponse>.Success(new LoginUserResponse
        {
            Email = userFound.Email,
            Id = userFound.Id,
            Name = userFound.Name,
            Token = string.Empty,
        });
    }
}
