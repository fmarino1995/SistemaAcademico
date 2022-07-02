using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Enderecos.Commands
{
    public class CriarEnderecoCommand : IRequest<Response<Endereco>>
    {
        public Endereco Endereco { get; set; }
    }

    public class CriarEnderecoCommandHandler : IRequestHandler<CriarEnderecoCommand, Response<Endereco>>
    {
        private readonly SistemaAcademicoContext _context;

        public CriarEnderecoCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Endereco>> Handle(CriarEnderecoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Endereco == null)
                    throw new ArgumentNullException(nameof(request.Endereco));

                Endereco Endereco = request.Endereco;

                _context.Enderecos.Add(Endereco);
                await _context.SaveChangesAsync();
                return new Response<Endereco>(Endereco);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<Endereco>();
                errorResponse.AddError(ex.Message);
                return errorResponse;
            }
        }
    }
}
