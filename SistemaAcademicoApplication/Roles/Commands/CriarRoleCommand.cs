using MediatR;
using Microsoft.AspNetCore.Identity;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Roles.Commands
{
    public class CriarRoleCommand : IRequest<Response<bool>>
    {
        public IdentityRole Role { get; set; }
    }

    public class CriarRoleCommandHandler : IRequestHandler<CriarRoleCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public CriarRoleCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(CriarRoleCommand request, CancellationToken cancellationToken)
        {
            var errorRessponse = new Response<bool>(false);

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (request.Role == null)
                        throw new ArgumentNullException(nameof(request.Role));

                    _context.Add(request.Role);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync(cancellationToken);

                    return new Response<bool>(true);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    errorRessponse.AddError("Erro ao criar perfil : " + ex.Message);
                    return errorRessponse;
                }
            }
        }
    }
}
