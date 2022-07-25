using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValidationEntities
{
    public class AddDisciplinaValidator : AbstractValidator<Disciplina>
    {
        public AddDisciplinaValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite um nome para a disciplina");

            RuleFor(x => x.Turno)
                .NotEmpty()
                .NotNull()
                .WithMessage("Escolha o turno da disciplina");

            RuleFor(x => x.ProfessorId)
                .NotEqual(0)
                .WithMessage("Escolha o professor associado a disciplina");
        }
    }
}
