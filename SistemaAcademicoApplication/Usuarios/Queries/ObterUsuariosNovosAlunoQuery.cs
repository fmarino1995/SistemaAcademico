using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Usuarios.Queries
{
    public class ObterUsuariosNovosAlunoQuery : IRequest<Response<List<ApplicationUser>>>
    {
    }

    public class ObterUsuariosNovosAlunoQueryHandler : IRequestHandler<ObterUsuariosNovosAlunoQuery, Response<List<ApplicationUser>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterUsuariosNovosAlunoQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }
        public async Task<Response<List<ApplicationUser>>> Handle(ObterUsuariosNovosAlunoQuery request, CancellationToken cancellationToken)
        {
            var emailUsuario = await (from u in _context.Users
                                      select u.Email).ToListAsync();

            var emailAluno = await (from p in _context.Alunos
                                        select p.Email).ToListAsync();

            var emailsOnly = emailUsuario.Except(emailAluno).ToList();

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
