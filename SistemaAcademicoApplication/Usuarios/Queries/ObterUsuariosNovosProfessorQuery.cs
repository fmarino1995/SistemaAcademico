using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Usuarios.Queries
{
    public class ObterUsuariosNovosProfessorQuery : IRequest<Response<List<ApplicationUser>>>
    {
    }

    public class ObterUsuariosNovosProfessorQueryHandler : IRequestHandler<ObterUsuariosNovosProfessorQuery, Response<List<ApplicationUser>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterUsuariosNovosProfessorQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<ApplicationUser>>> Handle(ObterUsuariosNovosProfessorQuery request, CancellationToken cancellationToken)
        {
            var emailUsuario = await (from u in _context.Users
                                      select u.Email).ToListAsync();

            var emailProfessor = await (from p in _context.Professores
                                        select p.Email).ToListAsync();

            var emailsOnly = emailUsuario.Except(emailProfessor).ToList();

            var usersList = new List<ApplicationUser>();

            foreach (var email in emailsOnly)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                usersList.Add(user);
            }

            return new Response<List<ApplicationUser>>(usersList);
        }
    }
}
