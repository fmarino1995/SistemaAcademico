using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Roles.Commands
{
    public class RemoverRoleCommand : IRequest<Response<bool>>
    {
        public string RoleId { get; set; }
    }

    public class RemoverRoleCommandHandler : IRequestHandler<RemoverRoleCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public RemoverRoleCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(RemoverRoleCommand request, CancellationToken cancellationToken)
        {
            var errorResponse = new Response<bool>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (string.IsNullOrEmpty(request.RoleId))
                        throw new ArgumentException("Id do perfil nulo", nameof(request.RoleId));

                    var role = await _context.Roles.FindAsync(request.RoleId);

                    _context.Remove(role);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync(cancellationToken);
                    return new Response<bool>(true);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    errorResponse.AddError("Erro ao excluir o perfil : " + ex.Message);
                    return errorResponse;
                }
            }
        }
    }
}
