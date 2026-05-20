using Market.Application.Modules.Auth.Commands.Address.Create;
using Market.Application.Modules.Auth.Commands.Address.Update;
using Market.Application.Modules.Auth.Commands.Address.Delete;
using Market.Application.Modules.Auth.Commands.Address.Read.GetById;
using Market.Application.Modules.Auth.Commands.Address.Read.GetAll;
using Market.Application.Modules.Auth.Commands.Address.Read.GetByFilter;


namespace Market.API.Controllers;

[ApiController]
[Route("Address")]
public sealed class AddressController(IMediator mediator) : ControllerBase
{
    [HttpPost("Create")]
    [AllowAnonymous]
    public async Task<ActionResult<CreateAddressCommandDto>> Create([FromBody] CreateAddressCommand command,
       CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpPut("Update")]
    [AllowAnonymous]
    public async Task<ActionResult> Update([FromBody] UpdateAddressCommand command,
       CancellationToken ct)
    {
        await mediator.Send(command, ct);
        return NoContent();
    }

    [HttpDelete("Delete")]
    [AllowAnonymous]
    public async Task<ActionResult> Delete([FromQuery] int addressId, CancellationToken ct)
    {
        await mediator.Send(new DeleteAddressCommand { AddressID = addressId }, ct);
        return NoContent();
    }

    [HttpGet("ById")]
    [AllowAnonymous]
    public async Task<ActionResult<GetByIdAddressQuery>> GetById([FromQuery] int addressId, CancellationToken ct)
    {
        return Ok(await mediator.Send(new GetByIdAddressQuery { AddressID = addressId }, ct));
    }

    [HttpGet("All")]
    [AllowAnonymous]
    public async Task<ActionResult<List<GetAllAddressQueryDto>>> GetAll([FromQuery] GetAllAddressQuery query, CancellationToken ct)
    {
        return Ok(await mediator.Send(query, ct));
    }

    [HttpGet("ByFilter")]
    [AllowAnonymous]
    public async Task<ActionResult<List<GetByFilterAddressQueryDto>>> GetByFilter([FromQuery] GetByFilterAddressQuery query, CancellationToken ct)
    {
        return Ok(await mediator.Send(query, ct));
    }

}
