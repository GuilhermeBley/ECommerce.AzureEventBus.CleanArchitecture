using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Ecommerce.Identity.Application.Security;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.User.GetUser;

public class GetUserHandler
    : IAppRequestHandler<GetUserRequest, Result<GetUserResponse?>>
{
    private readonly IClaimProvider _claimProvider;
    private readonly IdentityContext _identityContext;

    public GetUserHandler(IClaimProvider claimProvider, IdentityContext identityContext)
    {
        _claimProvider = claimProvider;
        _identityContext = identityContext;
    }

    public async Task<Result<GetUserResponse?>> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var userId = (await _claimProvider.GetCurrentAsync())?.GetUserId();

        var userFound = await _identityContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (userFound is null)
            return Result.Success<GetUserResponse?>();

        return Result.Success<GetUserResponse?>(
            new GetUserResponse
            {
                Email = userFound.Email,
                EmailConfirmed = userFound.EmailConfirmed,
                Id = userFound.Id,
                Name = userFound.Name,
                NickName = userFound.NickName,
                PhoneNumber = userFound.PhoneNumber
            }    
        );
    }
}
