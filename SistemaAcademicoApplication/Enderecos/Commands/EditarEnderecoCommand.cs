using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Enderecos.Commands
{
    public class EditarEnderecoCommand : IRequest<Response<Endereco>>
    {
        public Endereco Endereco { get; set; }
    }

    public class EditarEnderecoCommandHandler : IRequestHandler<EditarEnderecoCommand, Response<Endereco>>
    {
        private readonly SistemaAcademicoContext _context;

        public EditarEnderecoCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<Endereco>> Handle(EditarEnderecoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Endereco == null)
                    throw new ArgumentNullException(nameof(request.Endereco));

                _context.Enderecos.Update(request.Endereco);
                await _context.SaveChangesAsync();
                return new Response<Endereco>(request.Endereco);
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
