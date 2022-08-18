using SistemaAcademicoApplication.Common.Responses;
using MediatR;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Queries
{
    public class ObterPautaWebProfessorQuery : IRequest<Response<List<DisciplinaAluno>>>
    {
        public int DisciplinaId { get; set; }
        public int ProfessorId { get; set; }
        public string Turno { get; set; }
        public int SemestreVigenteId { get; set; }
    }

    public class ObterPautaWebProfessorQueryHandler : IRequestHandler<ObterPautaWebProfessorQuery, Response<List<DisciplinaAluno>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterPautaWebProfessorQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<DisciplinaAluno>>> Handle(ObterPautaWebProfessorQuery request, CancellationToken cancellationToken)
        {
            var pautaWeb = await _context.DisciplinasAlunos
                .Include(d => d.Disciplina)
                .Include(d => d.Aluno)
                .Include(d => d.SemestreVigente)
                .Where(d => d.Disciplina.ProfessorId == request.ProfessorId
                && d.DisciplinaId == request.DisciplinaId
                && d.SemestreVigenteId == request.SemestreVigenteId && d.Disciplina.Turno == request.Turno)
                .ToListAsync();

            return new Response<List<DisciplinaAluno>>(pautaWeb);
        }
    }
}
