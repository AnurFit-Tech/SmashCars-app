namespace Market.Application.Modules.User.Queries.GetUserList;

public class GetUserListQueryDto
{
    public string Firstname { get; init; }
    public string Lastname { get; init; }
    public string Email { get; init; }
    public bool IsEnabled { get; init; }

}

