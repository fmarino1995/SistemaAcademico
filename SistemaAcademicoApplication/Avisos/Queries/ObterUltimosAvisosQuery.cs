using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using System.Collections.Generic;

namespace SistemaAcademicoApplication.Avisos.Queries
{
    public class ObterUltimosAvisosQuery : IRequest<Response<List<Aviso>>>
    {
        public string EmailUsuario { get; set; }
    }

    public class ObterUltimosAvisosQueryHandler : IRequestHandler<ObterUltimosAvisosQuery, Response<List<Aviso>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterUltimosAvisosQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Aviso>>> Handle(ObterUltimosAvisosQuery request, CancellationToken cancellationToken)
        {
            var avisos = await _context.Avisos
                .Include(a => a.ApplicationUser)
                .OrderByDescending(a => a.DataCriacao)
                .Where(a => a.ApplicationUser.Email == request.EmailUsuario)
                .ToListAsync();


            if (avisos == null)
            {
                var errorResponse = new Response<List<Aviso>>();
                errorResponse.AddError("Nenhum aviso encontrado");
                return errorResponse;
            }

            return new Response<List<Aviso>>(avisos);
        }
    }
}
