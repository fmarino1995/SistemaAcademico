using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace SistemaAcademicoApplication.Disciplinas.Queries
{
    public class ObterDisciplinasQuery : IRequest<Response<List<Disciplina>>>
    {
    }

    public class ObterDisciplinasQueryHandler : IRequestHandler<ObterDisciplinasQuery, Response<List<Disciplina>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterDisciplinasQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Disciplina>>> Handle(ObterDisciplinasQuery request, CancellationToken cancellationToken)
        {
            var disciplinas = await _context.Disciplinas
                .Include(d => d.Professor)
                .OrderBy(d => d.Turno)
                .ThenBy(d => d.Nome)
                .ToListAsync();

            if (disciplinas == null)
            {
                var errorResponse = new Response<List<Disciplina>>();
                errorResponse.AddError("Nenhuma disciplina encontrada");
                return errorResponse;
            }

            return new Response<List<Disciplina>>(disciplinas);
        }
    }
}
