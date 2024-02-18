using MediatR;
using Microsoft.AspNetCore.Mvc;
using My.Application.UseCases.GetAccounts;
using My.Domain.Contracts;
using My.Domain.Enums;

namespace My.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExternalSystemsProbeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IFeatureFlag _featureFlag;

    public ExternalSystemsProbeController(IMediator mediator, IFeatureFlag featureFlag)
    {
        _mediator = mediator;
        _featureFlag = featureFlag;
    }

    [HttpGet]
    [Route("Accounts-legacy")]
    public async Task<ActionResult> GetAccountsLegacy()
    {
        var accounts = await _mediator.Send(new GetAccountsQueryLegacy()).ConfigureAwait(true);
        return Ok(accounts);
    }

    [HttpGet]
    [Route("Accounts-modern")]
    public async Task<ActionResult> GetAccountsModern()
    {
        var accounts = await _mediator.Send(new GetAccountsQueryModern()).ConfigureAwait(true);
        return Ok(accounts);
    }

    [HttpGet]
    [Route("FeatureFlag-IsLegacyDefault")]
    public ActionResult GetIsLegacyDefault()
    {
        var isLegacy = _featureFlag.IsFeatureEnabled(FeatureFlag.FeatureDefaultSystemLegacy);
        return Ok(isLegacy);
    }

    [HttpPost]
    [Route("FeatureFlag-LegacyDefaultToggle")]
    public ActionResult LegacyDefaultToggle()
    {
        _featureFlag.ToggleFeatureFlag(FeatureFlag.FeatureDefaultSystemLegacy);
        return Ok();
    }
}
