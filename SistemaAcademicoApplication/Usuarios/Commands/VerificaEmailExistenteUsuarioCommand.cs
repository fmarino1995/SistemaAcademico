using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Usuarios.Commands
{
    public class VerificaEmailExistenteUsuarioCommand : IRequest<Response<bool>>
    {
        public string UserEmail { get; set; }
    }

    public class VerificaEmailExistenteUsuarioCommandHandler : IRequestHandler<VerificaEmailExistenteUsuarioCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public VerificaEmailExistenteUsuarioCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(VerificaEmailExistenteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var errorResponse = new Response<bool>(false);

            if(string.IsNullOrEmpty(request.UserEmail))
            {
                errorResponse.AddError("Email não pode ser nulo");
                return errorResponse;
            }

            return _context.Users.Any(u => u.Email == request.UserEmail) ?  new Response<bool>(true) : new Response<bool>(false);
        }
    }
}
