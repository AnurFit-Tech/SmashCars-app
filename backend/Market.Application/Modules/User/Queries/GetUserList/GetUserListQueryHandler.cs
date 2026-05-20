namespace Market.Application.Modules.User.Queries.GetUserList;

public class GetUserListQueryHandler(IAppDbContext context)
    : IRequestHandler<GetUserListQuery, List<GetUserListQueryDto>>
{
    public async Task<List<GetUserListQueryDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var users = await context.Users
            .Where(x => !x.IsDeleted && x.IsAdmin==false && x.IsEmployee==false && x.IsManager==false)
            .Select(x => new GetUserListQueryDto
            {
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                IsEnabled = x.IsEnabled
            })
            .ToListAsync(cancellationToken);

        if (users == null || users.Count == 0)
        {
            throw new MarketNotFoundException("Nema registrovanih korisnika.");
        }

        return users;
    }
}
