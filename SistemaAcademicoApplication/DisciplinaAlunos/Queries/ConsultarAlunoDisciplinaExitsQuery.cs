using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Queries
{
    public class ConsultarAlunoDisciplinaExitsQuery : IRequest<Response<DisciplinaAluno>>
    {
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public int SemestreVigenteId { get; set; }
    }

    public class ConsultarAlunoDisciplinaExitsQueryHandler : IRequestHandler<ConsultarAlunoDisciplinaExitsQuery, Response<DisciplinaAluno>>
    {
        private readonly SistemaAcademicoContext _context;

        public ConsultarAlunoDisciplinaExitsQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<DisciplinaAluno>> Handle(ConsultarAlunoDisciplinaExitsQuery request, CancellationToken cancellationToken)
        {
            var alunoDisciplina = await _context.DisciplinasAlunos
                .Where(a => a.AlunoId == request.AlunoId
                && a.DisciplinaId == request.DisciplinaId
                && a.SemestreVigenteId == request.SemestreVigenteId)
                .FirstOrDefaultAsync();

            if (alunoDisciplina == null)
            {
                var errorResponse = new Response<DisciplinaAluno>();
                errorResponse.AddError("Registro não encontrado no banco de dados");
                return errorResponse;
            }

            return new Response<DisciplinaAluno>(alunoDisciplina);
        }
    }
}
