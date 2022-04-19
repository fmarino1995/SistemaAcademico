using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Usuarios.Queries
{
    public class ObterUsuarioQuery : IRequest<Response<ApplicationUser>>
    {
        public string UserId { get; set; }
    }

    public class ObterUsuarioQueryHandler : IRequestHandler<ObterUsuarioQuery, Response<ApplicationUser>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterUsuarioQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<ApplicationUser>> Handle(ObterUsuarioQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(request.UserId).ToString());

            if(user == null)
            {
                var errorResponse = new Response<ApplicationUser>();
                errorResponse.AddError("Usuário não encontrado");
                return errorResponse;
            }

            return new Response<ApplicationUser>(user);
        }
    }
}
