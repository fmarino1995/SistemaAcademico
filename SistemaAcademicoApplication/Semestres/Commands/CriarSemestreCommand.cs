using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Semestres.Commands
{
    public class CriarSemestreCommand : IRequest<Response<SemestreVigente>>
    {
        public SemestreVigente SemestreVigente { get; set; }
    }

    public class CriarSemestreCommandHandler : IRequestHandler<CriarSemestreCommand, Response<SemestreVigente>>
    {
        private readonly SistemaAcademicoContext _context;

        public CriarSemestreCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<SemestreVigente>> Handle(CriarSemestreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                SemestreVigente Semestre = request.SemestreVigente;
                _context.SemestresVigentes.Add(Semestre);
                await _context.SaveChangesAsync();
                return new Response<SemestreVigente>(Semestre);
            }
            catch (Exception)
            {
                var errorResponse = new Response<SemestreVigente>();
                errorResponse.AddError("Ocorreu um erro ao criar o registro");
                return errorResponse;
            }
        }
    }
}
