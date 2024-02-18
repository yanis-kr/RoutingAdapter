using MediatR;
using Microsoft.AspNetCore.Mvc;
using My.Application.UseCases.AddAccount;
using My.Application.UseCases.GetAccountById;
using My.Application.UseCases.GetAccounts;
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
        var accounts = await _mediator.Send(new GetAccountsQuery()).ConfigureAwait(true);

        return Ok(accounts);
    }

    [HttpGet("{id:int}", Name = "GetAccountById")]
    public async Task<ActionResult> GetAccountById(int id)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id)).ConfigureAwait(true);

        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult<DomainAccountResponse>> AddAccount([FromBody] DomainAccount account)
    {
        var result = await _mediator.Send(new AddAccountCommand(account)).ConfigureAwait(true);

        await _mediator.Publish(new AddAccountAddedNotification(result.Account)).ConfigureAwait(true);

        //return CreatedAtRoute("GetAccountById", new { id = result.Id }, result);
        return Ok(result);
    }
}
