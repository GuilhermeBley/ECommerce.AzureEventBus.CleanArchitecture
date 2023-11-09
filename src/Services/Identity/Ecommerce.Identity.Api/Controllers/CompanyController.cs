using Ecommerce.Identity.Application.Commands.Company.CreateCompany;
using Ecommerce.Identity.Application.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly IAppMediator _appMediator;

    public CompanyController(IAppMediator appMediator)
    {
        _appMediator = appMediator;
    }

    public async Task<ActionResult> CreateAsync(
        CreateCompanyRequest model,
        CancellationToken cancellationToken = default)
    {
        var result = await _appMediator.Send<CreateCompanyRequest, Result<CreateCompanyResponse>>(model, cancellationToken);

        if (result.TryGetValue(out var resultValue))
            return Created($"api/Company/{resultValue.Id}", resultValue);

        if (result.Errors.FirstOrDefault()?.Code == (int)HttpStatusCode.Conflict)
            return Conflict(result.Errors);

        return BadRequest(result.Errors);
    }
}
