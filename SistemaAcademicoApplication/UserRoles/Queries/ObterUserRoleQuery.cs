using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.UserRoles.Queries
{
    public class ObterUserRoleQuery : IRequest<Response<IdentityUserRole<string>>>
    {
        public string UserId { get; set; }
    }

    public class ObterUserRoleQueryHandler : IRequestHandler<ObterUserRoleQuery, Response<IdentityUserRole<string>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterUserRoleQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<IdentityUserRole<string>>> Handle(ObterUserRoleQuery request, CancellationToken cancellationToken)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == request.UserId);

            if (userRole == null)
            {
                var errorResponse = new Response<IdentityUserRole<string>>();
                errorResponse.AddError("Perfil de usuário não encontrado");
                return errorResponse;
            }

            return new Response<IdentityUserRole<string>>(userRole);
        }
    }
}
