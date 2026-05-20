using Market.Application.Modules.Auth.Commands.Register;
public sealed class RegisterCommandHandler(
    IAppDbContext ctx,
    IPasswordHasher<UserEntity> hasher)
    : IRequestHandler<RegisterCommand, RegisterCommandDto>
{
    public async Task<RegisterCommandDto> Handle(RegisterCommand request, CancellationToken ct)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var exists = await ctx.Users.AnyAsync(x => x.Email.ToLower() == email && !x.IsDeleted, ct);
        if (exists) throw new MarketConflictException("Korisnik sa tim emailom već postoji.");

        var user = new UserEntity
        {
            Firstname = request.Firstname.Trim(),
            Lastname = request.Lastname.Trim(),
            Email = email,
            IsEnabled = true,
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow
        };
<<<<<<< HEAD
        user.PasswordHash = hasher.HashPassword(user, request.Password);
=======
        user.PasswordHash = hasher.HashPassword(user, request.PasswordHash);
>>>>>>> 6a48888646c86c501baeded77ea33ff6a281d23f

        ctx.Users.Add(user);
        await ctx.SaveChangesAsync(ct);

        return new RegisterCommandDto { UserID = user.Id };
    }
}
