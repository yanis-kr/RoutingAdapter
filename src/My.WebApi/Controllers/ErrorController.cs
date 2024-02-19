using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace My.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ErrorController : ControllerBase
{
    private readonly IMediator _mediator;

    public ErrorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Error/RaiseError/500
    [HttpGet("RaiseError/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RaiseError(int id)
    {
        var query = new TestErrorQuery(id);
        var result = await _mediator.Send(query).ConfigureAwait(true);

        return Ok(result);
    }
}
