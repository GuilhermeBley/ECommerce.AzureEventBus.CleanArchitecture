using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;

namespace Ecommerce.Identity.Application.Commands.User.CreateUser;

public class CreateUserHandler : IAppRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
{
    private readonly IdentityContext _identityContext;

    public CreateUserHandler(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }

    public Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
