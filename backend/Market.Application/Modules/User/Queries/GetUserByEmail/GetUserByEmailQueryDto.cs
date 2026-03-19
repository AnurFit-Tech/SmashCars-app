namespace Market.Application.Modules.User.Queries.GetUserByEmail;

public class GetUserByEmailQueryDto
{
    public string Firstname { get; init; }
    public string Lastname { get; init; }
    public string Email { get; init; }
    public bool IsEnabled { get; init; }
   
}

