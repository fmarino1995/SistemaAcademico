using Domain.Entities;
using FluentValidation;

namespace Domain.ValidationEntities
{
    public class AddAlunoValidator : AbstractValidator<Aluno>
    {
        public AddAlunoValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite o cpf do aluno");

            RuleFor(x => x.Matricula)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite a matrícula do aluno");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite o nome do aluno");

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite a data de nascimento do aluno");

            RuleFor(x => x.ApplicationUserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Selecione o e-mail do aluno");

            RuleFor(x => x.Endereco.Logradouro)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite o logradouro");

            RuleFor(x => x.Endereco.Numero)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite o número");

            RuleFor(x => x.Endereco.Cidade)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite a cidade");

            RuleFor(x => x.Endereco.Bairro)
                .NotEmpty()
                .NotNull()
                .WithMessage("Digite o bairro");
        }
    }
}
