using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using MediatR;
using Domain.Entities;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Disciplinas.Commands
{
    public class ExcluirAlunoDisciplinaCommand : IRequest<Response<bool>>
    {
        public int DisciplinaAlunoId { get; set; }
    }

    public class ExcluirAlunoDisciplinaCommandHandler : IRequestHandler<ExcluirAlunoDisciplinaCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public ExcluirAlunoDisciplinaCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(ExcluirAlunoDisciplinaCommand request, CancellationToken cancellationToken)
        {
            var registro = await _context.DisciplinasAlunos.FindAsync(request.DisciplinaAlunoId);

            if (registro == null)
            {
                var errorResponse = new Response<bool>(false);
                errorResponse.AddError("Registro de aluno não encontrado");
                return errorResponse;
            }

            registro.Excluido = true;

            _context.DisciplinasAlunos.Update(registro);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
    }
}
