using Microsoft.EntityFrameworkCore;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Domain.Constantes;

namespace SistemaAcademicoApplication.Usuarios.Commands
{
    public class VerificarUsuarioAtivoCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; }
    }

    public class VerificarUsuarioAtivoCommandHandler : IRequestHandler<VerificarUsuarioAtivoCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public VerificarUsuarioAtivoCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(VerificarUsuarioAtivoCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                var errorResponse = new Response<bool>();
                errorResponse.AddError("Usuário não encontrado");
                return errorResponse;
            }

            if (user.Status == Parametros.StatusAtivo)
                return new Response<bool>(true);
            else
                return new Response<bool>(false);
        }
    }
}
