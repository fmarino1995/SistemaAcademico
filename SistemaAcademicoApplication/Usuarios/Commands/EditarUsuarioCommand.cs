using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Usuarios.Commands
{
    public class EditarUsuarioCommand : IRequest<Response<bool>>
    {
        public ApplicationUser User { get; set; }
    }

    public class EditarUsuarioCommandHandler : IRequestHandler<EditarUsuarioCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public EditarUsuarioCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(EditarUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.User == null)
                    throw new ArgumentNullException(nameof(request.User));

                _context.Users.Update(request.User);
                await _context.SaveChangesAsync();
                return new Response<bool>(true);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<bool>(false);
                errorResponse.AddError("Erro ao editar usuário : " + ex.Message);
                return errorResponse;
            }
        }
    }
}
