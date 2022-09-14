using FluentValidation;

namespace Rcm.Services.Users.Core.Commands.Validators;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(v => v.Email)
            .MinimumLength(6).WithMessage("Email не менее 6 символов")
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(v => v.Password)
            .MinimumLength(7).WithMessage("Пароль не менее 7 символов")
            .MaximumLength(100)
            .NotEmpty();
    }
}
