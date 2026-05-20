namespace Market.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommand : IRequest<RegisterCommandDto>
{
    public string Firstname { get; init; }
    public string Lastname { get; init; }
    public string Email { get; init; }
<<<<<<< HEAD
    public string Password { get; init; }
=======
    public string PasswordHash { get; init; }

>>>>>>> 6a48888646c86c501baeded77ea33ff6a281d23f
}
