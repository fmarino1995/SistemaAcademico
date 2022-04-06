using MediatR;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Roles.Commands
{
    public class EditarRoleCommand : IRequest<Response<bool>>
    {
        public Role Role { get; set; }
    }

    public class EditarRoleCommandHandler : IRequestHandler<EditarRoleCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;

        public EditarRoleCommandHandler(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(EditarRoleCommand request, CancellationToken cancellationToken)
        {
            var errorResponse = new Response<bool>(false);

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (request.Role == null)
                        throw new ArgumentException("Identificador do perfil não pode ser nulo", nameof(request.Role));

                    var identityRole = new IdentityRole
                    {
                        Id = request.Role.Id.ToString(),
                        Name = request.Role.Name,
                        NormalizedName = request.Role.NormalizedName,
                        ConcurrencyStamp = request.Role.ConcurrencyStamp.ToString()
                    };

                    _context.Roles.Update(identityRole);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);
                    return new Response<bool>(true);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    errorResponse.AddError("Erro ao editar o perfil : " + ex.Message);
                    return errorResponse;
                }
            }
        }
    }
}
