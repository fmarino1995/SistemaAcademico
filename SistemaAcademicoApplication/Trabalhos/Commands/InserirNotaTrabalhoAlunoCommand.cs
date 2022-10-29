using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Trabalhos.Commands
{
    public class InserirNotaTrabalhoAlunoCommand : IRequest<Response<bool>>
    {
        public Trabalho Trabalho { get; set; }
    }

    public class InserirNotaTrabalhoAlunoCommandHandler : IRequestHandler<InserirNotaTrabalhoAlunoCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public InserirNotaTrabalhoAlunoCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(InserirNotaTrabalhoAlunoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _context.Trabalhos.Update(request.Trabalho);
                await _context.SaveChangesAsync();
                return new Response<bool>(true);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<bool>(false);
                errorResponse.AddError("Erro ao inserir a nota no trabalho solicitado : " + ex.Message);
                return errorResponse;
            }
        }
    }
}
