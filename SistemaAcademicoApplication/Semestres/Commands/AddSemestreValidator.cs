using Domain.Entities;
using FluentValidation;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Semestres.Commands
{
    public class AddSemestreValidator : AbstractValidator<SemestreVigente>
    {
        private readonly SistemaAcademicoContext _context;

        public AddSemestreValidator(SistemaAcademicoContext context)
        {
            _context= context;

            RuleFor(x => x.DataInicioAux).NotEmpty().WithMessage("Selecione a data de início do semestre");

            RuleFor(x => x.DataFimAux).NotEmpty().WithMessage("Selecione a data de término do semestre");

            RuleFor(x => new { x.DataInicioAux, x.DataFimAux }).Must(x => CheckEqualYear(x.DataInicioAux, x.DataFimAux))
                .WithMessage("A data inicial e final precisam ser do mesmo ano");

            //RuleFor(x => x.DataFimAux).Must(CheckLastDateYear).WithMessage($"A data final precisar estar dentro do mesmo ano da data inicial");
            //RuleFor(x => x.DataInicioAux).Must(CheckBeginYearDate).WithMessage($"A data inicial precisar estar dentro do mesmo ano que a data final");

            RuleFor(x => x.DataInicioAux).Must(CheckBeginDate).WithMessage($"A data de início não pode ser menor que a data atual");

            RuleFor(x => x.DataInicioAux).Must(CheckEqualSemester).WithMessage("A data de início não pode estar entre as datas do semestre anterior.");
            RuleFor(x => x.DataFimAux).Must(CheckEqualSemester).WithMessage("A data final não pode estar entre as datas do semestre anterior");

            RuleFor(x => x.DataFimAux).Must(CheckSemestresCriados).WithMessage("Os dois semestres do ano selecionado já foram criados");
        }

        private bool CheckBeginDate(DateTime? date)
        {
            return date.HasValue && DateTime.Now.Date <= date.Value.Date;
        }

        private bool CheckSemestresCriados(DateTime? date)
        {
            return date.HasValue && !_context.SemestresVigentes.Any(x => x.DataFim.Date.Year == date.Value.Year && x.Semestre == 2);
        }

        private bool CheckBeginYearDate(DateTime? date)
        {
            return date.HasValue && DateTime.Now.Year == date.Value.Year;
        }

        private bool CheckLastDateYear(DateTime? date)
        {
            return date.HasValue && DateTime.Now.Year == date.Value.Year;
        }

        private bool CheckEqualYear(DateTime? dataIni, DateTime? dataFim)
        {
            return dataIni.HasValue && dataFim.HasValue && dataIni.Value.Year == dataFim.Value.Year;
        }

        private bool CheckEqualSemester(DateTime? date)
        {
            return date.HasValue && !_context.SemestresVigentes.Any(x => x.DataInicio.Date <= date.Value.Date && x.DataFim >= date.Value.Date);
        }
    }
}