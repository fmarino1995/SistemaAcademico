using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using MediatR;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Trabalhos.Queries
{
    public class ObterTrabalhosImportadosProfessorDisciplinaQuery : IRequest<Response<List<Trabalho>>>
    {
        public int ProfessorId { get; set; }
        public int DisciplinaId { get; set; }
        public string Turno { get; set; }
        public int SemestreVigenteId { get; set; }
    }

    public class ObterTrabalhosImportadosProfessorDisciplinaQueryHandler : IRequestHandler<ObterTrabalhosImportadosProfessorDisciplinaQuery, Response<List<Trabalho>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterTrabalhosImportadosProfessorDisciplinaQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Trabalho>>> Handle(ObterTrabalhosImportadosProfessorDisciplinaQuery request, CancellationToken cancellationToken)
        {
            var trabalhos = await _context.Trabalhos
                .Include(x => x.Disciplina)
                .Include(x => x.Aluno)
                .Include(x => x.SemestreVigente)
                .Where(x => x.Disciplina.ProfessorId == request.ProfessorId 
                && x.DisciplinaId == request.DisciplinaId
                && x.Disciplina.Turno == request.Turno
                && x.SemestreVigenteId == request.SemestreVigenteId)
                .ToListAsync();

            if (trabalhos == null)
            {
                var errorResponse = new Response<List<Trabalho>>();
                errorResponse.AddError("Nenhum trabalho encontrado");
                return errorResponse;
            }

            return new Response<List<Trabalho>>(trabalhos);
        }
    }
}
