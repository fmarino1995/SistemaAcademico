using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Alunos.Queries
{
    public class ObterAlunoEmailQuery : IRequest<Response<Aluno>>
    {
        public string Email { get; set; }
    }

    public class ObterAlunoEmailQueryHandler : IRequestHandler<ObterAlunoEmailQuery, Response<Aluno>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterAlunoEmailQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Aluno>> Handle(ObterAlunoEmailQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Email == request.Email);

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
