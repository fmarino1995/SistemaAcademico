using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Semestres.Queries
{
    public class ObterSemestresPautaWebAlunoQuery : IRequest<Response<List<SemestreVigente>>>
    {
        public int AlunoId { get; set; }
    }

    public class ObterSemestresPautaWebAlunoQueryHandler : IRequestHandler<ObterSemestresPautaWebAlunoQuery, Response<List<SemestreVigente>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterSemestresPautaWebAlunoQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<SemestreVigente>>> Handle(ObterSemestresPautaWebAlunoQuery request, CancellationToken cancellationToken)
        {
            var semestreAlunos = new List<SemestreVigente>();

            var semestres = await _context.DisciplinasAlunos
                .Include(s => s.Aluno)
                .Include(s => s.Disciplina)
                .Include(s => s.SemestreVigente)
                .Where(s => s.AlunoId == request.AlunoId)
                .ToListAsync();

            foreach (var semestre in semestres)
            {
                semestreAlunos.Add(semestre.SemestreVigente);
            }

            return new Response<List<SemestreVigente>>(semestreAlunos);
        }
    }
}
