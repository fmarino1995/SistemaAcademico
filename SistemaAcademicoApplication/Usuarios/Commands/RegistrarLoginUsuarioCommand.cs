using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Usuarios.Commands
{
    public class RegistrarLoginUsuarioCommand : IRequest<Response<bool>>
    {
        public string UserEmail { get; set; }
    }

    public class RegistrarLoginUsuarioCommandHandler : IRequestHandler<RegistrarLoginUsuarioCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public RegistrarLoginUsuarioCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(RegistrarLoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.UserEmail.Trim()); // Não pode ter espaço

            if (user == null)
            {
                var errorResponse = new Response<bool>(false);
                errorResponse.AddError("Usuário não encontrado. Tente novamente inserindo um email válido");
                return errorResponse;
            }

            user.DataAlteracao = DateTime.Now;
            user.UltimoLogin = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
    }
}
