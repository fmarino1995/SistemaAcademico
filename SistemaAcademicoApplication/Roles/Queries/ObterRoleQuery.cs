using MediatR;
using Microsoft.AspNetCore.Identity;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Roles.Queries
{
    public class ObterRoleQuery : IRequest<Response<IdentityRole>>
    {
        public string RoleId { get; set; }
    }

    public class ObterRoleQueryHandler : IRequestHandler<ObterRoleQuery, Response<IdentityRole>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterRoleQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<IdentityRole>> Handle(ObterRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return new Response<IdentityRole>(await _context.Roles.FindAsync(request.RoleId));
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<IdentityRole>();
                errorResponse.AddError("Erro ao encontrar o perfil" + ex.Message);
                return errorResponse;
            }
        }
    }
}
