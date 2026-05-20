using Market.Application.Modules.Auth.Commands.Card.Create;
using Market.Application.Modules.User.Queries.GetUserByEmail;
using Market.Application.Modules.User.Queries.GetUserList;

[ApiController]
[Route("api/users")]
public sealed class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("by-email")]
    [AllowAnonymous]
    public async Task<ActionResult<GetUserByEmailQueryDto>> GetByEmail([FromQuery] GetUserByEmailQuery query,CancellationToken ct)
    {
        return Ok(await mediator.Send(query, ct));
    }

    [HttpGet("list")]
    [AllowAnonymous]
    public async Task<ActionResult<List<GetUserListQueryDto>>> GetAll(CancellationToken ct)
    {
        return Ok(await mediator.Send(new GetUserListQuery(), ct));
    }
    [HttpPost("add-card")]
    [AllowAnonymous]
    public async Task<ActionResult<CreateCardCommandDto>> Create([FromBody] CreateCardCommand command,
       CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

}
