using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoApplication.Avisos.Commands
{
    public class CriarAvisoCommand : IRequest<Response<Aviso>>
    {
        public Aviso Aviso { get; set; }
    }

    public class CriarAvisoCommandHandler : IRequestHandler<CriarAvisoCommand, Response<Aviso>>
    {
        private readonly SistemaAcademicoContext _context;

        public CriarAvisoCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Aviso>> Handle(CriarAvisoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Aviso Aviso = request.Aviso;
                _context.Avisos.Add(Aviso);
                await _context.SaveChangesAsync();
                return new Response<Aviso>(Aviso);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<Aviso>();
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }
        }
    }
}
