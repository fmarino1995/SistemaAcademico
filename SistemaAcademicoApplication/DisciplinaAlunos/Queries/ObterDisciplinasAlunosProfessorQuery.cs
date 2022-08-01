using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ViewModels;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Queries
{
    public class ObterDisciplinasAlunosProfessorQuery : IRequest<Response<DisciplinaAlunoProfessorViewModel>>
    {
        public List<Disciplina> DisciplinasProfessorList { get; set; }
        public int Ano { get; set; }
    }

    public class ObterDisciplinasAlunosProfessorQueryHandler : IRequestHandler<ObterDisciplinasAlunosProfessorQuery, Response<DisciplinaAlunoProfessorViewModel>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterDisciplinasAlunosProfessorQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<DisciplinaAlunoProfessorViewModel>> Handle(ObterDisciplinasAlunosProfessorQuery request, CancellationToken cancellationToken)
        {
            //var query = await (from da in _context.DisciplinasAlunos
            //                   join d in _context.Disciplinas on da.DisciplinaId equals d.DisplicinaId
            //                   join na in _context.NotaAlunos on da.DisciplinasAlunosId equals na.DisciplinaAlunoId
            //                   where d.ProfessorId == request.ProfessorId && da.Ano == request.Ano)


            return new Response<DisciplinaAlunoProfessorViewModel>();
                               
        }
    }
}
