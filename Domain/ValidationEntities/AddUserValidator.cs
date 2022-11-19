using Domain.Entities;
using FluentValidation;

namespace Domain.ValidationEntities
{
    public class AddUserValidator : AbstractValidator<ApplicationUser>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Formato de e-mail inválido");

            RuleFor(x => x.NomeCompleto).NotEmpty().WithMessage("Digite o seu nome");

            RuleFor(x => x.RoleId).NotNull().WithMessage("Escolha um perfil para o usuário");
        }
    }
}
