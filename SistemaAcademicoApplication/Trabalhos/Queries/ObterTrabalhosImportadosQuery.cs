using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Trabalhos.Queries
{
    public class ObterTrabalhosImportadosQuery : IRequest<Response<List<Trabalho>>>
    {
        public int SemestreVigenteId { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
    }

    public class ObterTrabalhosImportadosQueryHandler : IRequestHandler<ObterTrabalhosImportadosQuery, Response<List<Trabalho>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterTrabalhosImportadosQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Trabalho>>> Handle(ObterTrabalhosImportadosQuery request, CancellationToken cancellationToken)
        {
            var trabalhos = await _context.Trabalhos
                .Include(t => t.Aluno)
                .Include(t => t.Disciplina)
                .Where(t => t.AlunoId == request.AlunoId
                && t.SemestreVigenteId == request.SemestreVigenteId
                && t.DisciplinaId == request.DisciplinaId)
                .ToListAsync();

            if (trabalhos == null)
            {
                var errorResponse = new Response<List<Trabalho>>();
                errorResponse.AddError("Nenhum trabalho encontrado");
                return errorResponse;
            }

            return new Response<List<Trabalho>>(trabalhos);
        }
    }
}
