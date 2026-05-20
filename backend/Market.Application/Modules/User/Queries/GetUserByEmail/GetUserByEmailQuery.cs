namespace Market.Application.Modules.User.Queries.GetUserByEmail;

public class GetUserByEmailQuery : IRequest<GetUserByEmailQueryDto>
{
    public string Email { get; set; }
}