using Ecommerce.Identity.Application.Commands.Authentication.LoginUser;
using Ecommerce.Identity.Application.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAppMediator _appMediator;

    public LoginController(IAppMediator appMediator)
    {
        _appMediator = appMediator;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(
        LoginUserRequest model, 
        CancellationToken cancellationToken = default)
    {
        var loginResult = await _appMediator.Send<LoginUserRequest, Result<LoginUserResponse>>(model, cancellationToken);

        if (loginResult.IsSuccess)
            return Ok(loginResult.Value);

        return Unauthorized();
    }
}