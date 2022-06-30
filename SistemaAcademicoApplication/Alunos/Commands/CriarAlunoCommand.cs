﻿using Domain.Entities;
using MediatR;
using SistemaAcademicoData.Context;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoApplication.Enderecos.Commands;

namespace SistemaAcademicoApplication.Alunos.Commands
{
    public class CriarAlunoCommand : IRequest<Response<Aluno>>
    {
        public Aluno Aluno { get; set; }
    }

    public class CriarAlunoCommandHandler : IRequestHandler<CriarAlunoCommand, Response<Aluno>>
    {
        private readonly SistemaAcademicoContext _context;
        private readonly IMediator _mediator;

        public CriarAlunoCommandHandler(SistemaAcademicoContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Response<Aluno>> Handle(CriarAlunoCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (request.Aluno == null)
                        throw new ArgumentNullException(nameof(request.Aluno));

                    Aluno Aluno = request.Aluno;

                    var enderecoCreate = await _mediator.Send(new CriarEnderecoCommand { Endereco = Aluno.Endereco });

                    if (enderecoCreate.Errors.Any())
                        throw new Exception("Erro ao salvar o endereço");

                    Aluno.EnderecoId = enderecoCreate.Result.EnderecoId;

                    _context.Alunos.Add(request.Aluno);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);
                    return new Response<Aluno>(Aluno);

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
