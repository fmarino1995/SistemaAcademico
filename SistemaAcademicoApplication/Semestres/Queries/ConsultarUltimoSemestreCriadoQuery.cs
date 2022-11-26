using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Semestres.Queries
{
    public class ConsultarUltimoSemestreCriadoQuery : IRequest<Response<SemestreVigente>>
    {
    }

    public class ConsultarUltimoSemestreCriadoQueryHandler : IRequestHandler<ConsultarUltimoSemestreCriadoQuery, Response<SemestreVigente>>
    {
        private readonly SistemaAcademicoContext _context;

        public ConsultarUltimoSemestreCriadoQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<SemestreVigente>> Handle(ConsultarUltimoSemestreCriadoQuery request, CancellationToken cancellationToken)
        {
            var ultimoSemestre = await _context.SemestresVigentes
                .OrderByDescending(s => s.SemestreVigenteId)
                .FirstOrDefaultAsync();

            if (ultimoSemestre == null)
            {
                var errorResponse = new Response<SemestreVigente>();
                errorResponse.AddError("Nenhum semestre encontrado");
                return errorResponse;
            }

            return new Response<SemestreVigente>(ultimoSemestre);
        }
    }
}
