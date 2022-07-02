using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Domain.ValidationEntities
{
    public class AddRoleValidator : AbstractValidator<IdentityRole>
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
