namespace Market.Application.Modules.Auth.Commands.Card.Create;

public sealed class CreateCardCommand : IRequest<CreateCardCommandDto>
{
    public string CardNumber { get; init; }
   
}
