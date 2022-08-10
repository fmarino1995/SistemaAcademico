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
    public class ObterDisciplinasAlunosProfessorQuery : IRequest<Response<List<DisciplinaAluno>>>
    {
        public int ProfessorId { get; set; }
        public int DisciplinaId { get; set; }
        public string Turno { get; set; }
    }

    public class ObterDisciplinasAlunosProfessorQueryHandler : IRequestHandler<ObterDisciplinasAlunosProfessorQuery, Response<List<DisciplinaAluno>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterDisciplinasAlunosProfessorQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<DisciplinaAluno>>> Handle(ObterDisciplinasAlunosProfessorQuery request, CancellationToken cancellationToken)
        {
            var semestre = DateTime.Now.Month <= 6 ? 1 : 2; // criar tabela para semestre vigente .


            var disciplinasAlunos = await _context.DisciplinasAlunos
                .Include(d => d.Disciplina)
                .Include(d => d.Aluno)
                .Where(d => d.DisciplinaId == request.DisciplinaId && d.Ano == DateTime.Now.Year && d.Disciplina.Turno == request.Turno)
                .ToListAsync();

            if (disciplinasAlunos == null)
            {
                var errorResponse = new Response<List<DisciplinaAluno>>();
                errorResponse.AddError("Nnehum registro encontrado");
                return errorResponse;
            }

            return new Response<List<DisciplinaAluno>>(disciplinasAlunos);
        }
    }
}
