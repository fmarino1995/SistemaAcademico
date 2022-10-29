using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Disciplinas.Queries
{
    public class ConsultarDisciplinaEPrerequisitoQuery : IRequest<Response<Disciplina>>
    {
        public string Matricula { get; set; }
        public string Turno { get; set; }
    }

    public class ConsultarDisciplinaEPrerequisitoQueryHandler : IRequestHandler<ConsultarDisciplinaEPrerequisitoQuery, Response<Disciplina>>
    {
        private readonly SistemaAcademicoContext _context;

        public ConsultarDisciplinaEPrerequisitoQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Disciplina>> Handle(ConsultarDisciplinaEPrerequisitoQuery request, CancellationToken cancellationToken)
        {
            var disciplina = await _context.Disciplinas
                .Where(d => d.PreRequisito == request.Matricula && d.Turno == request.Turno)
                .FirstOrDefaultAsync();

            if (disciplina == null)
            {
                var errorResponse = new Response<Disciplina>();
                errorResponse.AddError("Nenhuma disciplina pré-requisito encontrada");
                return errorResponse;
            }

            return new Response<Disciplina>(disciplina);
        }
    }
}
