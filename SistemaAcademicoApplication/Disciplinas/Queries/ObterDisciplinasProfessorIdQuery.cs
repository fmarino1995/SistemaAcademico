using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Disciplinas.Queries
{
    public class ObterDisciplinasProfessorIdQuery : IRequest<Response<List<Disciplina>>>
    {
        public int ProfessorId { get; set; }
    }

    public class ObterDisciplinasProfessorIdQueryHandler : IRequestHandler<ObterDisciplinasProfessorIdQuery, Response<List<Disciplina>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterDisciplinasProfessorIdQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Disciplina>>> Handle(ObterDisciplinasProfessorIdQuery request, CancellationToken cancellationToken)
        {
            var disciplinasProfessor = await _context.Disciplinas
                .Include(x => x.Professor)
                .Where(x => x.ProfessorId == request.ProfessorId)
                .ToListAsync();

            if (disciplinasProfessor == null)
            {
                var errorResponse = new Response<List<Disciplina>>();
                errorResponse.AddError("Nenhuma disciplina encontrada para o professor.");
                return errorResponse;
            }

            return new Response<List<Disciplina>>(disciplinasProfessor);
        }
    }
}
