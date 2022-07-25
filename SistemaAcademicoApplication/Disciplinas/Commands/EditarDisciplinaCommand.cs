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
    public class EditarDisciplinaCommand : IRequest<Response<Disciplina>>
    {
        public Disciplina Disciplina { get; set; }
    }

    public class EditarDisciplinaCommandHandler : IRequestHandler<EditarDisciplinaCommand, Response<Disciplina>>
    {
        private readonly SistemaAcademicoContext _context;

        public EditarDisciplinaCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Disciplina>> Handle(EditarDisciplinaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Disciplina == null)
                    throw new ArgumentException("Disciplina não pode ser nulo", nameof(request.Disciplina));

                Disciplina disciplina = request.Disciplina;

                _context.Disciplinas.Update(disciplina);
                await _context.SaveChangesAsync();
                return new Response<Disciplina>(disciplina);
            }
            catch (Exception ex)
            {
                var erroResponse = new Response<Disciplina>();
                erroResponse.AddError(ex.Message);
                return erroResponse;
            }
        }
    }
}
