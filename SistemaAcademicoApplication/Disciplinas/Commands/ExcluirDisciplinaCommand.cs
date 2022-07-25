using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Disciplinas.Commands
{
    public class ExcluirDisciplinaCommand : IRequest<Response<bool>>
    {
        public Disciplina Disciplina { get; set; }
    }

    public class ExcluirDisciplinaCommandHandler : IRequestHandler<ExcluirDisciplinaCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public ExcluirDisciplinaCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(ExcluirDisciplinaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Disciplina == null)
                    throw new ArgumentException("Disciplina não pode ser nulo", nameof(request.Disciplina));

                _context.Disciplinas.Remove(request.Disciplina);
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
