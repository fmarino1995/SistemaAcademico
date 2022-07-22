using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Professores.Queries
{
    public class VerificarProfessorEmailExistenteQuery : IRequest<Response<bool>>
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class VerificarProfessorEmailExistenteQueryHandler : IRequestHandler<VerificarProfessorEmailExistenteQuery, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public VerificarProfessorEmailExistenteQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(VerificarProfessorEmailExistenteQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {
                var professorExists = await _context.Professores.FirstOrDefaultAsync(a => a.ApplicationUserId == request.Id.Trim());

                if (professorExists != null)
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
                var professorExists = await _context.Professores.FirstOrDefaultAsync(a => a.Email == request.Email.Trim());

                if (professorExists != null)
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
