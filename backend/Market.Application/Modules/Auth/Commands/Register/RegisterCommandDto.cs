namespace Market.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommandDto
{
    public int UserID { get; set; }
    public string Email { get; set; }
    public string FirstName{get; set; }
}