using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValidationEntities
{
    public class AddProfessorValidator : AbstractValidator<Professor>
    {
        public AddProfessorValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage("Digite o cpf do professor");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Digite o nome do professor");

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .WithMessage("Digite a data de nascimento do professor");

            RuleFor(x => x.ApplicationUserId)
                .NotEmpty()
                .WithMessage("Selecione o e-mail do professor");

            RuleFor(x => x.Endereco.Logradouro)
                .NotEmpty()
                .WithMessage("Digite o logradouro");

            RuleFor(x => x.Endereco.Numero)
                .NotEmpty()
                .WithMessage("Digite o número");

            RuleFor(x => x.Endereco.Cidade)
                .NotEmpty()
                .WithMessage("Digite a cidade");

            RuleFor(x => x.Endereco.Bairro)
                .NotEmpty()
                .WithMessage("Digite o bairro");
        }
    }
}
