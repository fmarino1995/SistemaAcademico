using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Funcionarios.Queries
{
    public class ObterFuncionariosQuery : IRequest<Response<List<Funcionario>>>
    {
    }

    public class ObterFuncionariosQueryHandler : IRequestHandler<ObterFuncionariosQuery, Response<List<Funcionario>>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterFuncionariosQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Funcionario>>> Handle(ObterFuncionariosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var funcionarios = await _context.Funcionarios
                .Include(f => f.Endereco)
                .ToListAsync();

                if (funcionarios == null)
                    throw new ArgumentNullException(nameof(funcionarios));

                return new Response<List<Funcionario>>(funcionarios);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<List<Funcionario>>();
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }
        }
    }
}
