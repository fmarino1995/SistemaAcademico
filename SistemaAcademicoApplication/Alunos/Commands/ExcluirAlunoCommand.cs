using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using Domain.Constantes;
using SistemaAcademicoApplication.Usuarios.Commands;

namespace SistemaAcademicoApplication.Alunos.Commands
{
    public class ExcluirAlunoCommand : IRequest<Response<Aluno>>
    {
        public Aluno Aluno { get; set; }
    }

    public class ExcluirAlunoCommandHandler : IRequestHandler<ExcluirAlunoCommand, Response<Aluno>>
    {
        private readonly SistemaAcademicoContext _context;
        private readonly IMediator _mediator;

        public ExcluirAlunoCommandHandler(SistemaAcademicoContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Response<Aluno>> Handle(ExcluirAlunoCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (request.Aluno == null)
                        throw new ArgumentException("Aluno não encontrado para inativação", nameof(request.Aluno));

                    request.Aluno.Status = Parametros.StatusInativo;
                    _context.Alunos.Update(request.Aluno);
                    await _context.SaveChangesAsync();

                    var inativarUsuarioResponse = await _mediator.Send(new RemoverUsuarioCommand
                    {
                        UserId = request.Aluno.ApplicationUserId
                    });

                    if (inativarUsuarioResponse.Errors.Any())
                        throw new Exception("Erro ao inativar usuário associado ao aluno : " + inativarUsuarioResponse.Errors.First());

                    await transaction.CommitAsync(cancellationToken);
                    return new Response<Aluno>(request.Aluno);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    var errorResponse = new Response<Aluno>();
                    errorResponse.AddError(ex.Message);
                    return errorResponse;
                }
            }
        }
    }
}
