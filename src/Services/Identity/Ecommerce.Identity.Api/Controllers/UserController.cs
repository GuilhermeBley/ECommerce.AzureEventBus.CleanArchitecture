using Ecommerce.Identity.Application.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAppMediator _appMediator;

    public UserController(IAppMediator appMediator)
    {
        _appMediator = appMediator;
    }
}