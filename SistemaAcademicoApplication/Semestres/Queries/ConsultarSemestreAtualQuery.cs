using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Semestres.Queries
{
    public class ConsultarSemestreAtualQuery : IRequest<Response<SemestreVigente>>
    {
    }

    public class ConsultarSemestreAtualQueryHandler : IRequestHandler<ConsultarSemestreAtualQuery, Response<SemestreVigente>>
    {
        private readonly SistemaAcademicoContext _context;

        public ConsultarSemestreAtualQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<SemestreVigente>> Handle(ConsultarSemestreAtualQuery request, CancellationToken cancellationToken)
        {
            var semestreAtual = await _context.SemestresVigentes
                .OrderByDescending(s => s.Ano)
                .ThenByDescending(s => s.Semestre)
                .Where(s => s.Vigente)
                .FirstOrDefaultAsync();

            if(semestreAtual == null)
            {
                var errorResponse = new Response<SemestreVigente>();
                errorResponse.AddError("Nenhum semestre encontrado");
                return errorResponse;
            }

            return new Response<SemestreVigente>(semestreAtual);
        }
    }
}
