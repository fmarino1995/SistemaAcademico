using MediatR;
using SistemaAcademicoData.Context;
using SistemaAcademicoApplication.Common.Responses;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace SistemaAcademicoApplication.Alunos.Queries
{
    public class ObterAlunoQuery : IRequest<Response<Aluno>>
    {
        public int AlunoId { get; set; }
    }

    public class ObterAlunoQueryHandler : IRequestHandler<ObterAlunoQuery, Response<Aluno>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterAlunoQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Aluno>> Handle(ObterAlunoQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Endereco)
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(a => a.AlunoId == request.AlunoId);

            if (aluno == null)
            {
                var errorResponse = new Response<Aluno>();
                errorResponse.AddError("Aluno não encontrado");
                return errorResponse;
            }

            return new Response<Aluno>(aluno);
        }
    }
}
