using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Roles.Queries
{
    public class ObterRolesQuery : IRequest<Response<List<IdentityRole>>>
    {
    }

    public class ObterRolesQueryHandler : IRequestHandler<ObterRolesQuery, Response<List<IdentityRole>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterRolesQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<IdentityRole>>> Handle(ObterRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _context.Roles.ToListAsync();

            try
            {
                return new Response<List<IdentityRole>>(await _context.Roles.OrderBy(r => r.Name).ToListAsync());
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<List<IdentityRole>>();
                errorResponse.AddError("Erro ao consultar os perfis : " + ex.Message);
                return errorResponse;
            }
        }
    }
}
