using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Disciplinas.Queries
{
    public class ObterDisciplinaQuery : IRequest<Response<Disciplina>>
    {
        public int DisciplinaId { get; set; }
    }

    public class ObterDisciplinaQueryHandler : IRequestHandler<ObterDisciplinaQuery, Response<Disciplina>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterDisciplinaQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Disciplina>> Handle(ObterDisciplinaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var disciplina = await _context.Disciplinas
                    .Include(d => d.Professor)
                    .FirstOrDefaultAsync(d => d.DisciplinaId == request.DisciplinaId);

                if (disciplina == null)
                    throw new Exception("Disciplina não encontrada");

                return new Response<Disciplina>(disciplina);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<Disciplina>();
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }
        }
    }
}
