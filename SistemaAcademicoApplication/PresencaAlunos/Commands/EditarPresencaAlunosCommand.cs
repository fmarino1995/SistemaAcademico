using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using MediatR;
using SistemaAcademicoData.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.ViewModels;

namespace SistemaAcademicoApplication.PresencaAlunos.Commands
{
    public class EditarPresencaAlunosCommand : IRequest<Response<bool>>
    {
        public PresencaAlunosDisciplinaViewModel ViewModel { get; set; }
    }

    public class EditarPresencaAlunosCommandHandler : IRequestHandler<EditarPresencaAlunosCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public EditarPresencaAlunosCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(EditarPresencaAlunosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                foreach(var item in request.ViewModel.DisciplinasAlunoList)
                {
                    var faltaOld = request.ViewModel.PresencaAlunos
                        .Where(x => x.AlunoId == item.AlunoId).FirstOrDefault();

                    var presencaOld = request.ViewModel.PresencaAlunos
                        .Where(x => x.AlunoId == item.AlunoId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<bool>(false);
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }
        }

        private PresencaAluno FindPresencaAluno(int id)
        {
            return _context.PresencaAlunos
        }
    }
}
