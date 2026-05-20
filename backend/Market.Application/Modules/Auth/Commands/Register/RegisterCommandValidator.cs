using FluentValidation;

namespace Market.Application.Modules.Auth.Commands.Register;

/// <summary>
/// FluentValidation validator for <see cref="RegisterCommand"/>.
/// </summary>
public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name can be up to 50 characters long.");

        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name can be up to 50 characters long.");
       
    }
}
