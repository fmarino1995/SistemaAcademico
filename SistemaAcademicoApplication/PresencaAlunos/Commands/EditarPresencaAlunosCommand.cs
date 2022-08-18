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
                foreach (var item in request.ViewModel.DisciplinasAlunoList)
                {
                    var presencaOld = request.ViewModel.PresencaAlunos
                        .Where(x => x.AlunoId == item.AlunoId).FirstOrDefault();

                    if (item.IsFalta)
                    {
                        item.QuantidadePresenca--;
                        item.QuantidadeFalta++;
                        item.DataUltimaPresenca = new DateTime(0001, 01, 01);
                        presencaOld.Falta = item.IsFalta;
                        presencaOld.Presenca = false;
                    }

                    if (item.IsPresenca)
                    {
                        item.QuantidadeFalta--;
                        item.QuantidadePresenca++;
                        item.DataUltimaPresenca = DateTime.Now;
                        presencaOld.Presenca = item.IsPresenca;
                        presencaOld.Falta = false;
                    }

                    _context.DisciplinasAlunos.Update(item);
                    _context.PresencaAlunos.Update(presencaOld);
                }

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
