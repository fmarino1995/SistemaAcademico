using SistemaAcademicoApplication.Common.Responses;
using MediatR;
using SistemaAcademicoData.Context;
using Domain.Entities;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Commands
{
    public class GravarPresencaAlunosCommand : IRequest<Response<bool>>
    {
        public List<DisciplinaAluno> Alunos { get; set; }
    }

    public class GravarPresencaAlunosCommandHandler : IRequestHandler<GravarPresencaAlunosCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public GravarPresencaAlunosCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(GravarPresencaAlunosCommand request, CancellationToken cancellationToken)
        {
            if (request.Alunos == null || request.Alunos.Count == 0)
            {
                var errorResponse = new Response<bool>(false);
                errorResponse.AddError("Nenhum aluno encontrado");
                return errorResponse;
            }

            foreach (var item in request.Alunos.Where(x => x.Excluido == false && x.DataUltimaPresenca.Date != DateTime.Now.Date))
            {
                item.TotalAulasValidas++;

                if (item.IsSelected)
                {
                    item.QuantidadePresenca++;
                    item.DataUltimaPresenca = DateTime.Now;
                }
                else
                {
                    item.QuantidadeFalta++;
                }

                _context.DisciplinasAlunos.Update(item);
            }

            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
    }
}
