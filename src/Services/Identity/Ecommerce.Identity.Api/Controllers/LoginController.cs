using Ecommerce.Identity.Application.Commands.Authentication.LoginUser;
using Ecommerce.Identity.Application.Commands.Authentication.ResendEmailConfirmation;
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

        if (loginResult.TryGetValue(out var result))
            return Ok(result);

        return Unauthorized();
    }

    [HttpPost("resendEmailConfirmation")]
    public async Task<IActionResult> LoginAsync(
        ResendEmailConfirmationRequest model,
        CancellationToken cancellationToken = default)
    {
        var emailResendResult = await _appMediator.Send<ResendEmailConfirmationRequest, Result<ResendEmailConfirmationResponse>>(model, cancellationToken);

        if (emailResendResult.TryGetValue(out var result))
            return Ok(result);

        return BadRequest(emailResendResult.Errors);
    }
}