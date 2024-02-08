using MediatR;
using Microsoft.AspNetCore.Mvc;
using My.AppHandlers.Queries;

namespace My.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExternalSystemsProbeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExternalSystemsProbeController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Route("accounts-sys1")]
    public async Task<ActionResult> GetAccountsSys1()
    {
        var accounts = await _mediator.Send(new GetAccountsQuerySys1()).ConfigureAwait(true);
        return Ok(accounts);
    }

    [HttpGet]
    [Route("accounts-sys2")]
    public async Task<ActionResult> GetAccountsSys2()
    {
        var accounts = await _mediator.Send(new GetAccountsQuerySys2()).ConfigureAwait(true);
        return Ok(accounts);
    }

}
