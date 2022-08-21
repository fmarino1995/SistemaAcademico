using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace SistemaAcademicoApplication.PresencaAlunos.Queries
{
    public class ObterPresencaAlunosQuery : IRequest<Response<List<PresencaAluno>>>
    {
        public int SemestreVigenteId { get; set; }
    }

    public class ObterPresencaAlunosQueryHandler : IRequestHandler<ObterPresencaAlunosQuery, Response<List<PresencaAluno>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterPresencaAlunosQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<PresencaAluno>>> Handle(ObterPresencaAlunosQuery request, CancellationToken cancellationToken)
        {
            var presencaAlunos = await _context.PresencaAlunos
                .Where(p => p.DataPresenca.Date == DateTime.Now.Date
                && p.SemestreVigenteId == request.SemestreVigenteId)
                .ToListAsync();

            if (presencaAlunos == null)
            {
                var errorResponse = new Response<List<PresencaAluno>>();
                errorResponse.AddError("Nenhum registro de presença encontrado");
                return errorResponse;
            }

            return new Response<List<PresencaAluno>>(presencaAlunos);
        }
    }
}
