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
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Queries
{
    public class ObterDisciplinasAlunosProfessorQuery : IRequest<Response<DisciplinaAlunoProfessorViewModel>>
    {
        public int ProfessorId { get; set; }
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
            
                               

                               
                               


            return new Response<DisciplinaAlunoProfessorViewModel>();
                               
        }
    }
}
