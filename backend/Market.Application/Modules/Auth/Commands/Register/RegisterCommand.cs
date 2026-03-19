namespace Market.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommand : IRequest<RegisterCommandDto>
{
    public string Firstname { get; init; }
    public string Lastname { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }

}
