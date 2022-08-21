using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using Domain.Entities;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Commands
{
    public class AdicionarAlunoDisciplinaCommand : IRequest<Response<DisciplinaAluno>>
    {
        public DisciplinaAluno DisciplinaAluno { get; set; }
    }

    public class AdicionarAlunoDisciplinaCommandHandler : IRequestHandler<AdicionarAlunoDisciplinaCommand, Response<DisciplinaAluno>>
    {
        private readonly SistemaAcademicoContext _context;

        public AdicionarAlunoDisciplinaCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<DisciplinaAluno>> Handle(AdicionarAlunoDisciplinaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                DisciplinaAluno disciplinaAluno = request.DisciplinaAluno;

                _context.DisciplinasAlunos.Add(disciplinaAluno);
                await _context.SaveChangesAsync();
                return new Response<DisciplinaAluno>(disciplinaAluno);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<DisciplinaAluno>();
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }
        }
    }
}
