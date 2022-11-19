using Domain.Entities;
using FluentValidation;

namespace Domain.ValidationEntities
{
    public class AddAvisoValidator : AbstractValidator<Aviso>
    {
        public AddAvisoValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Digite o título do aviso");

            RuleFor(x => x.Texto)
                .NotEmpty().WithMessage("Digite o título do aviso");

            RuleFor(x => x.EmailDestino)
                .NotEmpty().WithMessage("Digite o título do aviso");
        }
    }
}
