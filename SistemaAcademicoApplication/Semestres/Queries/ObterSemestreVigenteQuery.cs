using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Semestres.Queries
{
    public class ObterSemestreVigenteQuery : IRequest<Response<SemestreVigente>>
    {
    }

    public class ObterSemestreVigenteQueryHandler : IRequestHandler<ObterSemestreVigenteQuery, Response<SemestreVigente>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterSemestreVigenteQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<SemestreVigente>> Handle(ObterSemestreVigenteQuery request, CancellationToken cancellationToken)
        {
            var semestreVigente = await _context.SemestresVigentes
                .FirstOrDefaultAsync(x => x.Vigente);

            if (semestreVigente == null)
            {
                var errorResponse = new Response<SemestreVigente>();
                errorResponse.AddError("Semestre vigente não encontrado");
                return errorResponse;
            }

            return new Response<SemestreVigente>(semestreVigente);
        }
    }
}
