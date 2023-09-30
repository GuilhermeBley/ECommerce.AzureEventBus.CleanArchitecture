using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Application.Security;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;

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

        var userClaims = await GetAllClaimsFromUserAsync(userFound.Id, cancellationToken);

        if (!userClaims.Any())
            return unauthorizedResult;

        var tokenGenerated = await _tokenProvider.CreateTokenAsync(userClaims, cancellationToken: cancellationToken);

        return Result<LoginUserResponse>.Success(new LoginUserResponse
        {
            Email = userFound.Email,
            Id = userFound.Id,
            Name = userFound.Name,
            Token = tokenGenerated,
        });
    }

    public async Task<Claim[]> GetAllClaimsFromUserAsync(Guid? userId, CancellationToken cancellationToken = default)
    {
        if (userId is null)
            return Array.Empty<Claim>();

        var userRoleClaims = await 
            (from role in _identityContext.Roles
             join roleUser in _identityContext.RoleUsersClaims
                on role.Id equals roleUser.RoleId
            join claim in _identityContext.RoleClaims
                on role.Id equals claim.IdRole
            where roleUser.Id == userId
            select new Claim(claim.ClaimType, claim.ClaimValue))
            .ToListAsync();

        var userClaims = await
            (from userClaim in _identityContext.UserClaims
             where userClaim.UserId == userId
             select new Claim(userClaim.ClaimType, userClaim.ClaimValue))
             .ToListAsync();

        var nonDuplicatedValues = new Dictionary<string, Claim>();

        userClaims.ForEach(claim => nonDuplicatedValues.TryAdd(
            string.Concat(claim.Type, "-", claim.Value),
            claim
        ));

        userRoleClaims.ForEach(claim => nonDuplicatedValues.TryAdd(
            string.Concat(claim.Type, "-", claim.Value),
            claim
        ));

        return nonDuplicatedValues.Values.ToArray();
    }
}

