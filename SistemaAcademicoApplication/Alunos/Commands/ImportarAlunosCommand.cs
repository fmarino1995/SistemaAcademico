using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoApplication.Common.Responses;
using MediatR;
using Domain.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Domain.Entities;
using Domain.Constantes;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using SistemaAcademicoApplication.Enderecos.Commands;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoApplication.LogImportacoes.Commands;

namespace SistemaAcademicoApplication.Alunos.Commands
{
    public class ImportarAlunosCommand : IRequest<Response<ImportarAlunosViewModel>>
    {
        public string FilePath { get; set; }
        public IBrowserFile File { get; set; }
    }

    public class ImportarAlunosCommandHandler : IRequestHandler<ImportarAlunosCommand, Response<ImportarAlunosViewModel>>
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public ImportarAlunosCommandHandler(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public async Task<Response<ImportarAlunosViewModel>> Handle(ImportarAlunosCommand request, CancellationToken cancellationToken)
        {
            DateTime dateResult;
            var errorResponse = new Response<ImportarAlunosViewModel>();
            ImportarAlunosViewModel viewModel = new ImportarAlunosViewModel();
            viewModel.LogImportacao = new LogImportacao();
            viewModel.LogImportacao.Errors = new List<string>();
            viewModel.LogImportacao.TipoImportacao = Parametros.TipoImportacaoAluno;
            viewModel.LogImportacao.DataCriacao = DateTime.Now;
            viewModel.LogImportacao.CaminhoArquivo = request.FilePath;
            viewModel.LogImportacao.NomeArquivo = request.File.Name;

            var importacaoFolder = new DirectoryInfo("C:\\arquivosImportacaoAluno");

            if (!importacaoFolder.Exists)
                importacaoFolder.Create();

            try
            {
                Stream stream = request.File.OpenReadStream();
                FileStream fs = File.Create(request.FilePath);
                await stream.CopyToAsync(fs);
                stream.Close();
                fs.Close();

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    Encoding = Encoding.Latin1
                };

                using (var reader = new StreamReader(request.FilePath, Encoding.Latin1))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        try
                        {
                            var cpfAluno = csv.GetField("CPF");

                            var novoAluno = new Aluno
                            {
                                Cpf = cpfAluno,
                                Nome = csv.GetField("NOME"),
                                Matricula = csv.GetField("MATRICULA"),
                                DataNascimento = DateTime.TryParse(csv.GetField("DATA_NASCIMENTO"), out dateResult) ? dateResult : throw new Exception($"Formato incorreto para data de nascimento do aluno de cpf '{cpfAluno}', não foi possível importar."),
                                Email = await VerificarEmailAlunoExits(csv.GetField("EMAIL_USUARIO")) ? throw new Exception($"Email informado para aluno de cpf '{cpfAluno}' já asssociado a outra conta, não foi possivel importar") : csv.GetField("EMAIL"),
                                DataHoraCadastro = DateTime.Now,
                                Status = Parametros.StatusAtivo,
                                UsuarioCriacao = await _currentUserService.GetUserNameAsync(),
                                Endereco = new Endereco
                                {
                                    CEP = csv.GetField("CEP").Replace("-", ""),
                                    Logradouro = csv.GetField("LOGRADOURO"),
                                    Numero = csv.GetField("NUMERO"),
                                    Complemento = csv.GetField("COMPLEMENTO"),
                                    Bairro = csv.GetField("BAIRRO"),
                                    Cidade = csv.GetField("CIDADE"),
                                    EstadoUF = csv.GetField("UF").Trim().Length > 2 ? throw new Exception($"UF deve apenas conter 2 caracteres para o aluno de cff : '{cpfAluno}', não foi possível improtar") : csv.GetField("UF")
                                },
                            };

                            var alunoResponse = await _mediator.Send(new CriarAlunoCommand { Aluno = novoAluno });

                            if (alunoResponse.Errors.Any())
                                throw new Exception($"Aluno de CPF '{cpfAluno}' não importado devido a um erro : " + alunoResponse.Errors.First());

                            viewModel.QuantidadeImportados++;
                        }
                        catch (Exception ex)
                        {
                            viewModel.LogImportacao.Errors.Add(ex.Message);
                            viewModel.QuantidadeNaoImportados++;
                            continue;
                        }
                    }
                }

                

                if (viewModel.LogImportacao.Errors.Any())
                {
                    foreach (var error in viewModel.LogImportacao.Errors)
                    {
                        viewModel.LogImportacao.Mensagem += string.Format("{0}\r\n", error);
                        viewModel.LogImportacao.Status = "ERRO";
                    }
                }
                else
                {
                    viewModel.LogImportacao.Status = "CONCLUIDO";
                    viewModel.LogImportacao.Mensagem = "Importação concluída sem erros para o arquivo " + request.File.Name;
                }

                await _mediator.Send(new CriarLogImportacaoCommand
                {
                    LogImportacao = viewModel.LogImportacao
                });

                return new Response<ImportarAlunosViewModel>(viewModel);
            }
            catch (CsvHelper.MissingFieldException)
            {
                errorResponse.AddError("Cabeçalho do modelo não está no formato correto. Importação foi cancelada.");
                return errorResponse;
            }
            catch (Exception ex)
            {
                errorResponse.AddError("Erro crítico no processo de importação : " + ex.Message);
                return errorResponse;
            }
        }

        private async Task<bool> VerificarEmailAlunoExits(string email)
        {
            var response = await _mediator.Send(new VerificarAlunoEmailExistenteCommand { Email = email });

            if (response.Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
