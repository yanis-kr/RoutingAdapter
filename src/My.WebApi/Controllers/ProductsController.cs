using MediatR;
using Microsoft.AspNetCore.Mvc;
using My.AppHandlers.Commands;
using My.AppHandlers.Notifications;
using My.AppHandlers.Queries;
using My.Domain.Models.Domain;

namespace My.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult> GetAccounts()
    {
        var accounts = await _mediator.Send(new GetAccountsQuery());

        return Ok(accounts);
    }

    [HttpGet("{id:int}", Name = "GetAccountById")]
    public async Task<ActionResult> GetAccountById(int id)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult> AddAccount([FromBody] DomainAccount account)
    {
        var accountToReturn = await _mediator.Send(new AddAccountCommand(account));

        await _mediator.Publish(new AccountAddedNotification(accountToReturn));

        return CreatedAtRoute("GetAccountById", new { id = accountToReturn.Id }, accountToReturn);
    }
}
