using SistemaAcademicoApplication.Common.Responses;
using MediatR;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Microsoft.AspNetCore.Components.Forms;

namespace SistemaAcademicoApplication.Trabalhos.Commands
{
    public class ImportarTrabalhoAlunoCommand : IRequest<Response<Trabalho>>
    {
        public Trabalho Trabalho { get; set; }
    }

    public class ImportarTrabalhoAlunoCommandHandler : IRequestHandler<ImportarTrabalhoAlunoCommand, Response<Trabalho>>
    {
        private readonly SistemaAcademicoContext _context;

        public ImportarTrabalhoAlunoCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Trabalho>> Handle(ImportarTrabalhoAlunoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Trabalho Trabalho = request.Trabalho;
                _context.Trabalhos.Add(Trabalho);
                await _context.SaveChangesAsync();
                return new Response<Trabalho>(Trabalho);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<Trabalho>();
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }
        }
    }
}
