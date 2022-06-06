using Domain.Entities;
using FluentValidation;

namespace Domain.ValidationEntities
{
    public class AddRoleValidator : AbstractValidator<Role>
    {
        public AddRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite um nome para o perfil");
        }
    }
}
