using Domain.Entities;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoApplication.Enderecos.Commands;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoData.Context;

namespace SistemaAcademicoApplication.Funcionarios.Commands
{
    public class CriarFuncionarioCommand : IRequest<Response<bool>>
    {
        public Funcionario Funcionario { get; set; }
    }

    public class CriarFuncionarioCommandHandler : IRequestHandler<CriarFuncionarioCommand, Response<bool>>
    {
        private readonly SistemaAcademicoContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public CriarFuncionarioCommandHandler(SistemaAcademicoContext context, ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(CriarFuncionarioCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (request.Funcionario == null)
                        throw new ArgumentNullException(nameof(request.Funcionario));

                    Funcionario func = request.Funcionario;

                    func.DataHoraCadastro = DateTime.Now;
                    func.UsuarioCriacao = await _currentUserService.GetUserNameAsync();
                    func.Status = "A";

                    var enderecoResponse = await _mediator.Send(new CriarEnderecoCommand { Endereco = request.Funcionario.Endereco });

                    if (enderecoResponse.Errors.Any())
                        throw new Exception("Erro ao registrar o endereço do funcionário : " + enderecoResponse.Errors.First());

                    _context.Funcionarios.Add(func);
                    await _context.SaveChangesAsync();
                    return new Response<bool>(true);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    var errorResponse = new Response<bool>(false);
                    errorResponse.AddError(ex.Message);
                    return errorResponse;
                }
            }
        }
    }
}
