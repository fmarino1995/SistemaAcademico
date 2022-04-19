using Domain.Constantes;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Usuarios.Commands
{
    public class RemoverUsuarioCommand : IRequest<Response<bool>>
    {
        public string UserId { get; set; }
    }

    public class RemoverUsuarioCommandHandler : IRequestHandler<RemoverUsuarioCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public RemoverUsuarioCommandHandler(SistemaAcademicoContext context)
        { 
            _context = context;
        }

        public async Task<Response<bool>> Handle(RemoverUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserId))
                    throw new ArgumentNullException(nameof(request.UserId));

                var user = await _context.Users.FindAsync(Guid.Parse(request.UserId).ToString());

                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                user.Status = ConstantesLogin.StatusUsuarioInativo;
                user.DataAlteracao = DateTime.Now;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<bool>(false);
                errorResponse.AddError("Erro ao excluir usuário : " + ex.Message);
                return errorResponse;
            }
        }
    }
}
