using FluentValidation;

namespace Financeasy.Application.UseCases.UserCases.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.User.Email)
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.User.NewPassword)
                .NotEmpty().WithMessage("Nova senha é obrigatória.")
                .When(x => !string.IsNullOrWhiteSpace(x.User.OldPassword));

            RuleFor(x => x.User.OldPassword)
                .NotEmpty().WithMessage("Senha antiga é obrigatória.")
                .When(x => !string.IsNullOrWhiteSpace(x.User.NewPassword));
        }
    }
}