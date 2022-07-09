using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using SistemaAcademicoApplication.Enderecos.Commands;

namespace SistemaAcademicoApplication.Alunos.Commands
{
    public class EditarAlunoCommand : IRequest<Response<Aluno>>
    {
        public Aluno Aluno { get; set; }
    }

    public class EditarAlunoCommandHandler : IRequestHandler<EditarAlunoCommand, Response<Aluno>>
    {
        private readonly SistemaAcademicoContext _context;
        private readonly IMediator _mediator;

        public EditarAlunoCommandHandler(SistemaAcademicoContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Response<Aluno>> Handle(EditarAlunoCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (request.Aluno == null)
                        throw new ArgumentException(nameof(request.Aluno));

                    var enderecoEditResponse = await _mediator.Send(new EditarEnderecoCommand
                    {
                        Endereco = request.Aluno.Endereco
                    });

                    if (enderecoEditResponse.Errors.Any())
                        throw new Exception(enderecoEditResponse.Errors.First());

                    _context.Alunos.Update(request.Aluno);
                    await _context.SaveChangesAsync();
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
