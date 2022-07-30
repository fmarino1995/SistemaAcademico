using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Alunos.Commands
{
    public class VerificarAlunoEmailExistenteCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class VerificarAlunoEmailExistenteCommandHandler : IRequestHandler<VerificarAlunoEmailExistenteCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public VerificarAlunoEmailExistenteCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(VerificarAlunoEmailExistenteCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {
                var alunoExists = await _context.Alunos.FirstOrDefaultAsync(a => a.ApplicationUserId == request.Id.Trim());

                if (alunoExists != null)
                {
                    return new Response<bool>(true);
                }
                else
                {
                    return new Response<bool>(false);
                }
            }
            else if (!string.IsNullOrEmpty(request.Email))
            {
                var alunoExists = await _context.Alunos.FirstOrDefaultAsync(a => a.Email == request.Email.Trim());

                if (alunoExists != null)
                {
                    return new Response<bool>(true);
                }
                else
                {
                    return new Response<bool>(false);
                }
            }
            else
            {
                var errorResponse = new Response<bool>();
                errorResponse.AddError("Não foi passado nenhum parametro para consulta");
                return errorResponse;
            }
        }
    }
}
