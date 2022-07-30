using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Disciplinas.Commands
{
    public class CriarDisciplinaCommand : IRequest<Response<Disciplina>>
    {
        public Disciplina Disciplina { get; set; }
    }

    public class CriarDisciplinaCommandHandler : IRequestHandler<CriarDisciplinaCommand, Response<Disciplina>>
    {
        private readonly SistemaAcademicoContext _context;

        public CriarDisciplinaCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Disciplina>> Handle(CriarDisciplinaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Disciplina == null)
                    throw new ArgumentException("Disciplina não pode ner nulo", nameof(request.Disciplina));

                Disciplina disciplina = request.Disciplina;

                _context.Disciplinas.Add(disciplina);
                await _context.SaveChangesAsync();
                return new Response<Disciplina>(disciplina);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<Disciplina>();
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }
        }
    }
}
