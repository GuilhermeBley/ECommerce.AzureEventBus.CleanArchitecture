using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Application.Security;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.Authentication.LoginUser;

public class LoginUserHandler
    : IAppRequestHandler<LoginUserRequest, Result<LoginUserResponse>>
{
    private readonly IdentityContext _identityContext;
    private readonly ITokenProvider _tokenProvider;

    public LoginUserHandler(
        IdentityContext identityContext,
        ITokenProvider tokenProvider)
    {
        _identityContext = identityContext;
        _tokenProvider = tokenProvider;
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

        var;

        var tokenGenerated = _tokenProvider.CreateTokenAsync();

        return Result<LoginUserResponse>.Success(new LoginUserResponse
        {
            Email = userFound.Email,
            Id = userFound.Id,
            Name = userFound.Name,
            Token = string.Empty,
        });
    }
}
