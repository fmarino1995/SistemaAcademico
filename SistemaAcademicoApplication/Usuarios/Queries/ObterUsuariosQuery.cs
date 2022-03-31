using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SistemaAcademicoData.Context;
using Domain.Entities;

namespace SistemaAcademicoApplication.Usuarios.Queries
{
    public class ObterUsuariosQuery : IRequest<Response<List<ApplicationUser>>>
    {
    }

    public class ObterUsuariosQueryHandler : IRequestHandler<ObterUsuariosQuery, Response<List<ApplicationUser>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterUsuariosQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<ApplicationUser>>> Handle(ObterUsuariosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarios = await _context.Users
                    .OrderBy(u => u.UserName)
                    .ToListAsync();

                if (usuarios.Count == 0 || usuarios == null)
                    throw new Exception("Nenhum usuário encontrado");

                return new Response<List<ApplicationUser>>(usuarios);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<List<ApplicationUser>>();
                errorResponse.AddError("Erro ao trazer a lista de usuários: " + ex.Message);
                return errorResponse;
            }
        }
    }
}
