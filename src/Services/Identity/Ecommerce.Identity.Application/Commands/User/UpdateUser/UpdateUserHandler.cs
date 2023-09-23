using Ecommerce.Identity.Application.Mediator;
using Ecommerce.Identity.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Identity.Application.Commands.User.UpdateUser;

public class UpdateUserHandler : IAppRequestHandler<UpdateUserRequest, Result<UpdateUserResponse>>
{
    private readonly IdentityContext _context;

    public UpdateUserHandler(IdentityContext context)
    {
        _context = context;
    }

    public Task<Result<UpdateUserResponse>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        Ecommerce.Identity.Core.Entities.User.Create()
    }
}
