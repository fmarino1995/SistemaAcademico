using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.RespostaAvisos.Queries
{
    public class ObterRespostasAvisoQuery : IRequest<Response<List<RespostaAviso>>>
    {
        public int AvisoId { get; set; }
    }

    public class ObterRespostasAvisoQueryHandler : IRequestHandler<ObterRespostasAvisoQuery, Response<List<RespostaAviso>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterRespostasAvisoQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<RespostaAviso>>> Handle(ObterRespostasAvisoQuery request, CancellationToken cancellationToken)
        {
            var respostas = await _context.RespostaAvisos
                .Where(r => r.AvisoId == request.AvisoId)
                .OrderBy(r => r.DataHoraResposta)
                .ToListAsync();

            return new Response<List<RespostaAviso>>(respostas);
        }
    }
}
