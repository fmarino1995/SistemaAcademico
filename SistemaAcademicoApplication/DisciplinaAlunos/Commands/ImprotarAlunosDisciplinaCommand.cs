using System.Text;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using Domain.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using SistemaAcademicoData.Context;
using Domain.Entities;
using Domain.Constantes;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using SistemaAcademicoApplication.LogImportacoes.Commands;
using SistemaAcademicoApplication.Alunos.Queries;
using SistemaAcademicoApplication.Disciplinas.Queries;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Commands
{
    public class ImportarAlunosDisciplinaCommand : IRequest<Response<ImportacaoAlunosDisciplinaViewModel>>
    {
        public IBrowserFile File { get; set; }
        public string FilePath { get; set; }
        public int DisciplinaId { get; set; }
        public int Semestre { get; set; }
    }

    public class ImportarAlunosDisciplinaCommandHandler : IRequestHandler<ImportarAlunosDisciplinaCommand, Response<ImportacaoAlunosDisciplinaViewModel>>
    {
        private readonly IMediator _mediator;
        private readonly SistemaAcademicoContext _context;

        public ImportarAlunosDisciplinaCommandHandler(IMediator mediator, SistemaAcademicoContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<Response<ImportacaoAlunosDisciplinaViewModel>> Handle(ImportarAlunosDisciplinaCommand request, CancellationToken cancellationToken)
        {
            ImportacaoAlunosDisciplinaViewModel viewModel = new ImportacaoAlunosDisciplinaViewModel();
            viewModel.LogImportacao = new LogImportacao();
            viewModel.LogImportacao.Errors = new List<string>();
            viewModel.LogImportacao.TipoImportacao = Parametros.TipoImportacaoAlunoDisciplina;
            viewModel.LogImportacao.DataCriacao = DateTime.Now;
            viewModel.LogImportacao.CaminhoArquivo = request.FilePath;
            viewModel.LogImportacao.NomeArquivo = request.File.Name;

            Disciplina disciplina = (await GetDisciplinaAsync(request.DisciplinaId)).Result;

            var importacaoFolder = new DirectoryInfo("C:\\arquivosImportacaoAlunosDisciplina");

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
                    Encoding = Encoding.UTF8
                };

                using (var reader = new StreamReader(request.FilePath))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        try
                        {
                            var email = csv.GetField("EmailAluno");

                            var aluno = await GetAlunoAsync(email.Trim());

                            if (aluno.Errors.Any())
                            {
                                throw new Exception($"Aluno de email {email} não encontrado");
                            }

                            var disciplinaAluno = new DisciplinaAluno
                            {
                                AlunoId = aluno.Result.AlunoId,
                                DisciplinaId = request.DisciplinaId,
                                TotalAulasValidas = 0,
                                Ano = DateTime.Now.Year,
                                QuantidadeFalta = 0,
                                QuantidadePresenca = 0,
                                Semestre = request.Semestre
                            };

                            viewModel.EmailAlunos.Add(email);
                            _context.DisciplinasAlunos.Add(disciplinaAluno);
                            await _context.SaveChangesAsync();
                        }
                        catch (CsvHelper.MissingFieldException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            viewModel.LogImportacao.Errors.Add(ex.Message);
                            continue;
                        }
                    }
                }
            }
            catch (CsvHelper.MissingFieldException)
            {
                var errorResponse = new Response<ImportacaoAlunosDisciplinaViewModel>();
                errorResponse.AddError("Arquivo não está no formato correto. Tente novamente alterando o arquivo.");
                return errorResponse;
            }
            catch (Exception ex)
            {
                viewModel.LogImportacao.Mensagem = @"Erro ao realizar a importação" +
                    " Mensagem do sistema : " + ex.Message;
                viewModel.LogImportacao.Status = "ERRO";

                await _mediator.Send(new CriarLogImportacaoCommand { LogImportacao = viewModel.LogImportacao });

                return new Response<ImportacaoAlunosDisciplinaViewModel>(viewModel);
            }


            viewModel.LogImportacao.Status = "CONCLUIDO";
            viewModel.LogImportacao.Mensagem = viewModel.LogImportacao.Errors.Count == 0 ? $"Importação concluída sem erros do arquivo {request.File.Name}"
                : $"Importação concluída com {viewModel.LogImportacao.Errors.Count} erros no arquivo {request.File.Name}";
            await _mediator.Send(new CriarLogImportacaoCommand { LogImportacao = viewModel.LogImportacao });
            return new Response<ImportacaoAlunosDisciplinaViewModel>(viewModel);
        }

        private async Task<Response<Aluno>> GetAlunoAsync(string email)
        {
            return await _mediator.Send(new ObterAlunoEmailQuery { Email = email });
        }

        private async Task<Response<Disciplina>> GetDisciplinaAsync(int disciplinaId)
        {
            return await _mediator.Send(new ObterDisciplinaQuery { DisciplinaId = disciplinaId });
        }
    }
}
