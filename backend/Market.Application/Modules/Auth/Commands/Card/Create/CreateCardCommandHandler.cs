namespace Market.Application.Modules.Auth.Commands.Card.Create;

public sealed class CreateCardCommandHandler(
    IAppDbContext ctx,
    IAppCurrentUser currentUser
) : IRequestHandler<CreateCardCommand, CreateCardCommandDto>
{
    public async Task<CreateCardCommandDto> Handle(CreateCardCommand request,CancellationToken ct)
    {
        if (!currentUser.IsAuthenticated || currentUser.UserId is null)
            throw new UnauthorizedAccessException("Korisnik nije prijavljen.");

        var exists = await ctx.Card.AnyAsync(
            x => x.CardNumber == request.CardNumber
                 && x.UserID == currentUser.UserId.Value,ct);

        if (exists) throw new MarketConflictException("Kartica već postoji za ovog korisnika.");

        var card = new CardEntity
        {
            UserID = currentUser.UserId.Value,
            CardNumber = request.CardNumber
        };

        ctx.Card.Add(card);
        await ctx.SaveChangesAsync(ct);

        return new CreateCardCommandDto{ CardID = card.CardID };
    }
}
