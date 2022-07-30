using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Professores.Queries
{
    public class ObterProfessoresQuery : IRequest<Response<List<Professor>>>
    {
    }

    public class ObterProfessoresQueryHandler : IRequestHandler<ObterProfessoresQuery, Response<List<Professor>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterProfessoresQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Professor>>> Handle(ObterProfessoresQuery request, CancellationToken cancellationToken)
        {
            var professores = await _context.Professores
                .OrderBy(p => p.Nome)
                .ToListAsync();

            if (professores == null)
            {
                var errorResponse = new Response<List<Professor>>();
                errorResponse.AddError("Nenhum professor encontrado");
                return errorResponse;
            }

            return new Response<List<Professor>>(professores);
        }
    }
}
