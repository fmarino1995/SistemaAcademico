using MediatR;
using SistemaAcademicoData.Context;
using SistemaAcademicoApplication.Common.Responses;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace SistemaAcademicoApplication.Professores.Queries
{
    public class ObterProfessorQuery : IRequest<Response<Professor>>
    {
        public int ProfessorId { get; set; }
    }

    public class ObterProfessorQueryHandler : IRequestHandler<ObterProfessorQuery, Response<Professor>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterProfessorQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Professor>> Handle(ObterProfessorQuery request, CancellationToken cancellationToken)
        {
            var professor = await _context.Professores
                .Include(a => a.Endereco)
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(a => a.ProfessorId == request.ProfessorId);

            if (professor == null)
            {
                var errorResponse = new Response<Professor>();
                errorResponse.AddError("Professor não encontrado");
                return errorResponse;
            }

            return new Response<Professor>(professor);
        }
    }
}
