using SistemaAcademicoApplication.Common.Responses;
using MediatR;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Usuarios.Queries
{
    public class ObterEmailsUsuarioQuery : IRequest<Response<List<string>>>
    {
    }

    public class ObterEmailsUsuarioQueryHandler : IRequestHandler<ObterEmailsUsuarioQuery, Response<List<string>>>
    {
        private SistemaAcademicoContext _context;

        public ObterEmailsUsuarioQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<string>>> Handle(ObterEmailsUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _context.Users.ToListAsync();

            var emailsList = new List<string>();

            foreach (var usuario in usuarios)
            {
                emailsList.Add(usuario.Email);
            }

            return new Response<List<string>>(emailsList);
        }
    }
}
