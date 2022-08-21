using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Semestres.Commands
{
    public class AlterarSemestreVigenteCommand : IRequest<Response<SemestreVigente>>
    {
        public SemestreVigente SemestreAntigo { get; set; }
        public SemestreVigente SemestreNovo { get; set; }
    }

    public class AlterarSemestreVigenteCommandHandler : IRequestHandler<AlterarSemestreVigenteCommand, Response<SemestreVigente>>
    {
        private readonly SistemaAcademicoContext _context;

        public AlterarSemestreVigenteCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<SemestreVigente>> Handle(AlterarSemestreVigenteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                SemestreVigente NovoSemestre = request.SemestreNovo;
                _context.SemestresVigentes.Update(request.SemestreAntigo);
                _context.SemestresVigentes.Add(NovoSemestre);
                await _context.SaveChangesAsync();
                return new Response<SemestreVigente>(NovoSemestre);
            }
            catch (Exception)
            {
                var errorResponse = new Response<SemestreVigente>();
                errorResponse.AddError("Ocorreu um erro ao tentar adiconar o semestre.");
                return errorResponse;
            }
        }
    }
}
