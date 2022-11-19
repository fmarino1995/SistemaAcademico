using Domain.Entities;
using FluentValidation;

namespace SistemaAcademicoApplication.Alunos.Commands
{
    public class AddAlunoValidator : AbstractValidator<Aluno>
    {
        public AddAlunoValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage("Digite o cpf do aluno");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Digite o nome do aluno");

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .WithMessage("Digite a data de nascimento do aluno");

            RuleFor(x => x.ApplicationUserId)
                .NotEmpty()
                .WithMessage("Selecione o e-mail do aluno");

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
