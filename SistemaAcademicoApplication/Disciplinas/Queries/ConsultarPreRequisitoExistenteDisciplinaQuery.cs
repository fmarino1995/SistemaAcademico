using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Disciplinas.Queries
{
    public class ConsultarPreRequisitoExistenteDisciplinaQuery : IRequest<Response<Disciplina>>
    {
        public string MatriculaDisciplina { get; set; }
        public string Turno { get; set; }
    }

    public class ConsultarPreRequisitoExistenteDisciplinaQueryHandler : IRequestHandler<ConsultarPreRequisitoExistenteDisciplinaQuery, Response<Disciplina>>
    {
        private readonly SistemaAcademicoContext _context;

        public ConsultarPreRequisitoExistenteDisciplinaQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Disciplina>> Handle(ConsultarPreRequisitoExistenteDisciplinaQuery request, CancellationToken cancellationToken)
        {
            var disciplina = await _context.Disciplinas
                .FirstOrDefaultAsync(d => d.PreRequisito == request.MatriculaDisciplina && d.Turno == request.Turno);

            if (disciplina == null)
            {
                var errorResponse = new Response<Disciplina>();
                errorResponse.AddError("Nenhuma disicplina encontrada com esse pré-requisito");
                return errorResponse;
            }

            return new Response<Disciplina>(disciplina);
        }
    }
}
