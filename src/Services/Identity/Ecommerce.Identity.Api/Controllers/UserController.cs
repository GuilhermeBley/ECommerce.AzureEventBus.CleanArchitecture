using Ecommerce.Identity.Application.Commands.User.CreateUser;
using Ecommerce.Identity.Application.Commands.User.GetUser;
using Ecommerce.Identity.Application.Commands.User.UpdateUser;
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
    [AllowAnonymous]
    public async Task<ActionResult<CreateUserResponse>> CreateAsync(
        CreateUserRequest model, 
        CancellationToken cancellationToken = default)
    {
        var result = await _appMediator.Send<CreateUserRequest, Result<CreateUserResponse>>(model, cancellationToken);

        if (result.TryGetValue(out var value))
            return Created($"api/User/{value.Id}", value);

        return BadRequest(result.Errors);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<CreateUserResponse>> GetByIdAsync(
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        var result = await _appMediator.Send<GetUserRequest, Result<GetUserResponse>>(new GetUserRequest
        {
            UserId = userId
        }, cancellationToken);

        if (result.TryGetValue(out var value))
            return Ok(value);

        return BadRequest(result.Errors);
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<CreateUserResponse>> UpdateByIdAsync(
        Guid userId,
        UpdateUserRequest model, 
        CancellationToken cancellationToken = default)
    {
        model.IdToUpdate = userId;

        var result = await _appMediator.Send<UpdateUserRequest, Result<UpdateUserResponse>>(model, cancellationToken);

        if (result.TryGetValue(out var value))
            return Ok(value);

        return BadRequest(result.Errors);
    }
}