using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Semestres.Queries
{
    public class ObterSemestresQuery : IRequest<Response<List<SemestreVigente>>>
    {
    }

    public class ObterSemestresQueryHandler : IRequestHandler<ObterSemestresQuery, Response<List<SemestreVigente>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterSemestresQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<SemestreVigente>>> Handle(ObterSemestresQuery request, CancellationToken cancellationToken)
        {
            var semestres = await _context.SemestresVigentes
                .OrderByDescending(s => s.Ano)
                .ThenByDescending(s => s.Semestre)
                .ToListAsync();

            return new Response<List<SemestreVigente>>(semestres);
        }
    }
}
