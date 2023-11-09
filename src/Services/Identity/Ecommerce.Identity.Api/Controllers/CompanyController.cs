using Ecommerce.Identity.Application.Commands.Company.CreateCompany;
using Ecommerce.Identity.Application.Commands.Company.DisableCompany;
using Ecommerce.Identity.Application.Commands.Company.GetAllCompaniesFromUser;
using Ecommerce.Identity.Application.Commands.Company.UpdateCompanyName;
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

    [HttpPost]
    public async Task<ActionResult> CreateAsync(
        CreateCompanyRequest model,
        CancellationToken cancellationToken = default)
    {
        var result = await _appMediator.Send<CreateCompanyRequest, Result<CreateCompanyResponse>>(model, cancellationToken);

        if (result.TryGetValue(out var resultValue))
            return Created($"api/Company/{resultValue.Id}", resultValue);

        if (result.Errors.Any(r => r.Code == (int)HttpStatusCode.Conflict))
            return Conflict(result.Errors);

        return BadRequest(result.Errors);
    }

    [HttpDelete("{companyId}")]
    public async Task<ActionResult> DeleteAsync(
        Guid companyId,
        CancellationToken cancellationToken = default)
    {
        var result = await _appMediator.Send<DisableCompanyRequest, Result<DisableCompanyResponse>>(new DisableCompanyRequest
        {
            CompanyId = companyId
        }, cancellationToken);

        if (result.TryGetValue(out var resultValue))
            return Ok(resultValue);

        if (result.Errors.Any(r => r.Code == (int)HttpStatusCode.NotFound))
            return NotFound(result.Errors);

        return BadRequest(result.Errors);
    }

    [HttpPatch("Name/{companyId}")]
    public async Task<ActionResult> PatchNameAsync(
        Guid companyId,
        UpdateCompanyNameRequest model,
        CancellationToken cancellationToken = default)
    {
        model.CompanyId = companyId;

        var result = await _appMediator.Send<UpdateCompanyNameRequest, Result<UpdateCompanyNameResponse>>(model, cancellationToken);

        if (result.TryGetValue(out var resultValue))
            return Ok(resultValue);

        if (result.Errors.Any(r => r.Code == (int)HttpStatusCode.NotFound))
            return NotFound(result.Errors);

        return BadRequest(result.Errors);
    }

    [HttpGet("my-companies")]
    public async Task<ActionResult> GetAllCompaniesFromUserAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _appMediator.Send<GetAllCompaniesFromCurrentUserRequest, Result<GetAllCompaniesFromCurrentUserResponse>>(new GetAllCompaniesFromCurrentUserRequest { }, cancellationToken);

        if (result.TryGetValue(out var resultValue))
            return Ok(resultValue);

        if (result.Errors.Any(r => r.Code == (int)HttpStatusCode.NotFound))
            return NoContent();

        return BadRequest(result.Errors);
    }
}
