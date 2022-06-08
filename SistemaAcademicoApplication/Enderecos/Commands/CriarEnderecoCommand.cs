using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Enderecos.Commands
{
    public class CriarEnderecoCommand : IRequest<Response<bool>>
    {
        public Endereco Endereco { get; set; }
    }

    public class CriarEnderecoCommandHandler : IRequestHandler<CriarEnderecoCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public CriarEnderecoCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(CriarEnderecoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Endereco == null)
                    throw new ArgumentNullException(nameof(request.Endereco));

                _context.Enderecos.Add(request.Endereco);
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
