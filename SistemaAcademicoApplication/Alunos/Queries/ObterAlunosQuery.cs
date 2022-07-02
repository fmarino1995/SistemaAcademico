using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Alunos.Queries
{
    public class ObterAlunosQuery : IRequest<Response<List<Aluno>>>
    {
    }

    public class ObterAlunosQueryHandler : IRequestHandler<ObterAlunosQuery, Response<List<Aluno>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterAlunosQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Aluno>>> Handle(ObterAlunosQuery request, CancellationToken cancellationToken)
        {
            var alunos = await _context.Alunos
                .OrderBy(a => a.Nome)
                .ToListAsync();

            if (alunos == null)
            {
                var errorResponse = new Response<List<Aluno>>();
                errorResponse.AddError("Nenhum aluno encontrado");
                return errorResponse;
            }

            return new Response<List<Aluno>>(alunos);
        }
    }
}
