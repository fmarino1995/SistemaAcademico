using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Usuarios.Queries
{
    public class ConsultarEmailUsuarioQuery : IRequest<Response<ApplicationUser>>
    {
        public string Email { get; set; }
    }

    public class ConsultarEmailUsuarioQueryHandler : IRequestHandler<ConsultarEmailUsuarioQuery, Response<ApplicationUser>>
    {
        private readonly SistemaAcademicoContext _context;

        public ConsultarEmailUsuarioQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<ApplicationUser>> Handle(ConsultarEmailUsuarioQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                var errorResponse = new Response<ApplicationUser>();
                errorResponse.AddError("Nenhum usuario encontrado com o email passado");
                return errorResponse;
            }

            return new Response<ApplicationUser>(user);
        }
    }
}
