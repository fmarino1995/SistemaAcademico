using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using MediatR;
using Domain.Entities;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoApplication.Enderecos.Commands;
using SistemaAcademicoApplication.Usuarios.Queries;
using Domain.Constantes;

namespace SistemaAcademicoApplication.Professores.Commands
{
    public class CriarProfessorCommand : IRequest<Response<Professor>>
    {
        public Professor Professor { get; set; }
    }

    public class CriarProfessorCommandHandler : IRequestHandler<CriarProfessorCommand, Response<Professor>>
    {
        private readonly SistemaAcademicoContext _context;
        private readonly IMediator _mediator;

        public CriarProfessorCommandHandler(SistemaAcademicoContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Response<Professor>> Handle(CriarProfessorCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (request.Professor == null)
                        throw new ArgumentNullException(nameof(request.Professor));

                    Professor Professor = request.Professor;

                    var enderecoCreate = await _mediator.Send(new CriarEnderecoCommand { Endereco = Professor.Endereco });

                    if (enderecoCreate.Errors.Any())
                        throw new Exception("Erro ao salvar o endereço");

                    var appUser = await _mediator.Send(new ObterUsuarioQuery { UserId = Professor.ApplicationUserId });

                    if (appUser.Errors.Any())
                        throw new Exception("Erro ao associar o professor com a conta de usuário correspondente");

                    Professor.Email = appUser.Result.Email;
                    Professor.EnderecoId = enderecoCreate.Result.EnderecoId;
                    Professor.DataHoraCadastro = DateTime.Now;
                    Professor.Status = Parametros.StatusAtivo;

                    _context.Professores.Add(Professor);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);
                    return new Response<Professor>(Professor);

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
