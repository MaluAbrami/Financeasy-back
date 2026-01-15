using FluentValidation;

namespace Financeasy.Application.UseCases.UserCases.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.User.Email)
                .EmailAddress().WithMessage("Email inválido.");
        }
    }
}