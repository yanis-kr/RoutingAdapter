using MediatR;
using Microsoft.AspNetCore.Mvc;
using My.AppHandlers.Queries;
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
    [Route("Accounts-sys1")]
    public async Task<ActionResult> GetAccountsSys1()
    {
        var accounts = await _mediator.Send(new GetAccountsQuerySys1()).ConfigureAwait(true);
        return Ok(accounts);
    }

    [HttpGet]
    [Route("Accounts-sys2")]
    public async Task<ActionResult> GetAccountsSys2()
    {
        var accounts = await _mediator.Send(new GetAccountsQuerySys2()).ConfigureAwait(true);
        return Ok(accounts);
    }

    [HttpGet]
    [Route("FeatureFlag-IsSys1Default")]
    public ActionResult GetIsSys1Default()
    {
        var isSys1 = _featureFlag.IsFeatureEnabled(FeatureFlag.FeatureDefaultSystemSys1);
        return Ok(isSys1);
    }

    [HttpPost]
    [Route("FeatureFlag-Sys1DefaultToggle")]
    public ActionResult Sys1DefaultToggle()
    {
        _featureFlag.ToggleFeatureFlag(FeatureFlag.FeatureDefaultSystemSys1);
        return Ok();
    }
}
