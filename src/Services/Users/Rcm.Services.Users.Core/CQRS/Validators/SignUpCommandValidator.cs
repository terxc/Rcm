using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rcm.Services.Users.Core.DAL;

namespace Rcm.Services.Users.Core.CQRS.Validators;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    private readonly UsersDbContext _dbContext;

    public SignUpCommandValidator(UsersDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(v => v.Email)
            .MinimumLength(6).WithMessage("Email не менее 6 символов")
            .MaximumLength(100).WithMessage("Email не более 100 символов")
            .MustAsync(BeUniqueEmail).WithMessage("Email уже используется");

        RuleFor(v => v.Password)
            .MinimumLength(7).WithMessage("Пароль не менее 7 символов")
            .MaximumLength(100).WithMessage("Email не более 100 символов");
    }

    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email) == null;
    }
}
