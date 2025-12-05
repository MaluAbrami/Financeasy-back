using FluentValidation;

namespace Financeasy.Application.UseCases.Login
{
    public class LoginCommanndValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommanndValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres ou números");
        }
    }
}