using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValidationEntities
{
    public class AddUserValidator : AbstractValidator<ApplicationUser>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Formato de e-mail inválido");

            RuleFor(x => x.NomeCompleto).NotEmpty().NotNull().WithMessage("Digite o seu nome");
        }
    }
}
