namespace Market.Application.Modules.User.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler(IAppDbContext context)
    : IRequestHandler<GetUserByEmailQuery, GetUserByEmailQueryDto>
{
    public async Task<GetUserByEmailQueryDto> Handle(
        GetUserByEmailQuery request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var q = context.Users.Where(x => x.Email.ToLower() == email && !x.IsDeleted
        && x.IsAdmin == false && x.IsEmployee == false && x.IsManager == false);

        

        var dto = await q
            .Select(x => new GetUserByEmailQueryDto
            {
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                IsEnabled = x.IsEnabled
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (dto == null)
        {
            throw new MarketNotFoundException(
                $"User with email '{request.Email}' not found.");
        }

        return dto;
    }
}
