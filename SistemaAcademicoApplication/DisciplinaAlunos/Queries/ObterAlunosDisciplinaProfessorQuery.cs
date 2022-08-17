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
using SistemaAcademicoApplication.Semestres.Queries;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Queries
{
    public class ObterAlunosDisciplinaProfessorQuery : IRequest<Response<List<DisciplinaAluno>>>
    {
        public int ProfessorId { get; set; }
        public int DisciplinaId { get; set; }
        public string Turno { get; set; }
    }

    public class ObterAlunosDisciplinaProfessorQueryHandler : IRequestHandler<ObterAlunosDisciplinaProfessorQuery, Response<List<DisciplinaAluno>>>
    {
        private readonly SistemaAcademicoContext _context;
        private readonly IMediator _mediator;

        public ObterAlunosDisciplinaProfessorQueryHandler(SistemaAcademicoContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Response<List<DisciplinaAluno>>> Handle(ObterAlunosDisciplinaProfessorQuery request, CancellationToken cancellationToken)
        {
            var ultimoSemestre = await _mediator.Send(new ConsultarSemestreAtualQuery());
            
            var disciplinasAlunos = await _context.DisciplinasAlunos
                .Include(d => d.Disciplina)
                .Include(d => d.Aluno)
                .Where(d => d.DisciplinaId == request.DisciplinaId 
                && d.Ano == ultimoSemestre.Result.Ano 
                && d.Semestre == ultimoSemestre.Result.Semestre
                && d.Disciplina.Turno == request.Turno
                && !d.Excluido)
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
