using SistemaAcademicoApplication.Common.Responses;
using MediatR;
using Domain.Entities;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.DisciplinaAlunos
{
    public class AdicionarNotaAlunosCommand : IRequest<Response<bool>>
    {
        public List<DisciplinaAluno> Alunos { get; set; }
    }

    public class AdicionarNotaAlunosCommandHandler : IRequestHandler<AdicionarNotaAlunosCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public AdicionarNotaAlunosCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(AdicionarNotaAlunosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _context.DisciplinasAlunos.UpdateRange(request.Alunos);
                await _context.SaveChangesAsync();
                return new Response<bool>(true);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<bool>(false);
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }
        }
    }
}
