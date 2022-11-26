using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Semestres.Commands
{
    public class AlterarVigenciaSemestreCommand : IRequest<Response<bool>>
    {
        public SemestreVigente SemestreAtual { get; set; }
        public SemestreVigente SemestreNovo { get; set; }
    }

    public class AlterarVigenciaSemestreCommandHandler : IRequestHandler<AlterarVigenciaSemestreCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public AlterarVigenciaSemestreCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(AlterarVigenciaSemestreCommand request, CancellationToken cancellationToken)
        {
            request.SemestreNovo.Vigente = true;
            request.SemestreAtual.Vigente = false;

            _context.SemestresVigentes.Update(request.SemestreNovo);
            _context.SemestresVigentes.Update(request.SemestreAtual);
            await _context.SaveChangesAsync();

            return new Response<bool>(true);
        }
    }
}