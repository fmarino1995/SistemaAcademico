using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.LogImportacoes.Commands
{
    public class CriarLogImportacaoCommand : IRequest<Response<bool>>
    {
        public LogImportacao LogImportacao { get; set; }
    }

    public class CriarLogImportacaoCommandHandler : IRequestHandler<CriarLogImportacaoCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public CriarLogImportacaoCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(CriarLogImportacaoCommand request, CancellationToken cancellationToken)
        {
            if (request.LogImportacao == null)
            {
                var errorResponse = new Response<bool>(false);
                errorResponse.AddError("Erro ao criar log");
                return errorResponse;
            }

            _context.LogImportacoes.Add(request.LogImportacao);

            await _context.SaveChangesAsync();

            return new Response<bool>(true);
        }
    }
}
