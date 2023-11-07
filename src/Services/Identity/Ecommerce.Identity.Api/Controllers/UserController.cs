using Ecommerce.Identity.Application.Commands.User.CreateUser;
using Ecommerce.Identity.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IAppMediator _appMediator;

    public UserController(IAppMediator appMediator)
    {
        _appMediator = appMediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> CreateAsync(
        CreateUserRequest model, 
        CancellationToken cancellationToken = default)
    {
        var result = await _appMediator.Send<CreateUserRequest, Result<CreateUserResponse>>(model, cancellationToken);

        if (result.TryGetValue(out var value))
            return Created($"api/User/{value.Id}", value);

        return BadRequest(result.Errors);
    }
}