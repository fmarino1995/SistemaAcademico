using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Queries
{
    public class ObterPautaWebAlunoQuery : IRequest<Response<List<DisciplinaAluno>>>
    {
        public int AlunoId { get; set; }
    }

    public class ObterPautaWebAlunoQueryHandler : IRequestHandler<ObterPautaWebAlunoQuery, Response<List<DisciplinaAluno>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterPautaWebAlunoQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<DisciplinaAluno>>> Handle(ObterPautaWebAlunoQuery request, CancellationToken cancellationToken)
        {
            var pautaWeb = await _context.DisciplinasAlunos
                .Include(p => p.Disciplina)
                .Include(p => p.Aluno)
                .Where(p => p.AlunoId == request.AlunoId)
                .ToListAsync();

            return new Response<List<DisciplinaAluno>>(pautaWeb);
        }
    }
}
