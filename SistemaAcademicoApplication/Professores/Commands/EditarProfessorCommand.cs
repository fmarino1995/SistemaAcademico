using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.Entities;
using SistemaAcademicoData.Context;
using SistemaAcademicoApplication.Enderecos.Commands;

namespace SistemaAcademicoApplication.Professores.Commands
{
    public class EditarProfessorCommand : IRequest<Response<Professor>>
    {
        public Professor Professor { get; set; }
    }

    public class EditarProfessorCommandHandler : IRequestHandler<EditarProfessorCommand, Response<Professor>>
    {
        private readonly SistemaAcademicoContext _context;
        private readonly IMediator _mediator;

        public EditarProfessorCommandHandler(SistemaAcademicoContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Response<Professor>> Handle(EditarProfessorCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (request.Professor == null)
                        throw new ArgumentException(nameof(request.Professor));

                    var enderecoEditResponse = await _mediator.Send(new EditarEnderecoCommand
                    {
                        Endereco = request.Professor.Endereco
                    });

                    if (enderecoEditResponse.Errors.Any())
                        throw new Exception(enderecoEditResponse.Errors.First());

                    _context.Professores.Update(request.Professor);
                    await _context.SaveChangesAsync();
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
