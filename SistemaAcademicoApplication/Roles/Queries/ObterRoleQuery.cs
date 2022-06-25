using MediatR;
using Microsoft.AspNetCore.Identity;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

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
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId);

                if (role == null)
                    throw new ArgumentNullException(nameof(role));

                return new Response<IdentityRole>(role);
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
