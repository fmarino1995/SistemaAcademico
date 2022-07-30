using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Domain.Constantes;
using SistemaAcademicoApplication.Usuarios.Commands;

namespace SistemaAcademicoApplication.Professores.Commands
{
    public class ExcluirProfessorCommand : IRequest<Response<Professor>>
    {
        public Professor Professor { get; set; }
    }

    public class ExcluirProfessorCommandHandler : IRequestHandler<ExcluirProfessorCommand, Response<Professor>>
    {
        private readonly SistemaAcademicoContext _context;
        private readonly IMediator _mediator;

        public ExcluirProfessorCommandHandler(SistemaAcademicoContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Response<Professor>> Handle(ExcluirProfessorCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (request.Professor == null)
                        throw new ArgumentException("Professor não encontrado para inativação", nameof(request.Professor));

                    request.Professor.Status = Parametros.StatusInativo;
                    _context.Professores.Update(request.Professor);
                    await _context.SaveChangesAsync();

                    var inativarUsuarioResponse = await _mediator.Send(new RemoverUsuarioCommand
                    {
                        UserId = request.Professor.ApplicationUserId
                    });

                    if (inativarUsuarioResponse.Errors.Any())
                        throw new Exception("Erro ao inativar usuário associado ao professor : " + inativarUsuarioResponse.Errors.First());

                    await transaction.CommitAsync(cancellationToken);
                    return new Response<Professor>(request.Professor);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    var errorResponse = new Response<Professor>();
                    errorResponse.AddError(ex.Message);
                    return errorResponse;
                }
            }
        }
    }
}
