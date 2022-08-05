using MediatR;
using Microsoft.AspNetCore.Identity;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.UserRoles.Commands
{
    public class AssociarPerfilUsuarioCommand : IRequest<Response<bool>>
    {
        public IdentityUserRole<string> UserRole { get; set; }
    }

    public class AssociarPerfilUsuarioCommandHandler : IRequestHandler<AssociarPerfilUsuarioCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;
        private IMediator _mediator;

        public AssociarPerfilUsuarioCommandHandler(SistemaAcademicoContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(AssociarPerfilUsuarioCommand request, CancellationToken cancellationToken)
        {
            _context.UserRoles.Add(request.UserRole);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
    }
}
