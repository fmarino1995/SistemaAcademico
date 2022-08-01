using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoApplication.Professores.Queries
{
    public class ObterProfessorEmailQuery : IRequest<Response<Professor>>
    {
        public string Email { get; set; }
    }

    public class ObterProfessorEmailQueryHandler : IRequestHandler<ObterProfessorEmailQuery, Response<Professor>>
    {
        private readonly SistemaAcademicoContext _context;

        public ObterProfessorEmailQueryHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Professor>> Handle(ObterProfessorEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                    throw new ArgumentException("Email não pode ser nulo", nameof(request.Email));

                var professor = await _context.Professores.FirstOrDefaultAsync(x => x.Email == request.Email);

                if (professor == null)
                    throw new Exception("Nenhum professor encontrado com o email passado");

                return new Response<Professor>(professor);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<Professor>();
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }

        }
    }
}
