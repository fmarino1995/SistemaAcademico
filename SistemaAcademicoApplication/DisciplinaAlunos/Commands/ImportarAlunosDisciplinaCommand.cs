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
using SistemaAcademicoApplication.Semestres.Queries;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoApplication.DisciplinaAlunos.Queries;

namespace SistemaAcademicoApplication.DisciplinaAlunos.Commands
{
    public class ImportarAlunosDisciplinaCommand : IRequest<Response<ImportacaoAlunosDisciplinaViewModel>>
    {
        public IBrowserFile File { get; set; }
        public string FilePath { get; set; }
        public int DisciplinaId { get; set; }
    }

    public class ImportarAlunosDisciplinaCommandHandler : IRequestHandler<ImportarAlunosDisciplinaCommand, Response<ImportacaoAlunosDisciplinaViewModel>>
    {
        private readonly IMediator _mediator;
        private readonly SistemaAcademicoContext _context;
        private readonly IEmailService _emailService;

        public ImportarAlunosDisciplinaCommandHandler(IMediator mediator, SistemaAcademicoContext context, IEmailService emailService)
        {
            _mediator = mediator;
            _context = context;
            _emailService = emailService;
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
            viewModel.EmailAlunos = new List<string>();
            SemestreVigente SemestreAtual;

            Disciplina disciplina = (await GetDisciplinaAsync(request.DisciplinaId)).Result;

            var importacaoFolder = new DirectoryInfo("C:\\arquivosImportacaoAlunosDisciplina");

            if (!importacaoFolder.Exists)
                importacaoFolder.Create();

            try
            {
                var response = await _mediator.Send(new ConsultarSemestreAtualQuery());
                SemestreAtual = response.Result;

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
                                throw new Exception($"Aluno de email '{email}' não encontrado");
                            }

                            if(aluno.Result.Status == "I")
                            {
                                throw new Exception($"Aluno '{aluno.Result.Nome}' não pode ser adicionado a disciplina '{disciplina.Nome}'. Aluno está com status Inativo.");
                            }

                            if(disciplina.Turno != aluno.Result.Turno)
                            {
                                throw new Exception($"Aluno '{aluno.Result.Nome}' não pode ser adicionado a disciplina '{disciplina.Nome}'. O turno do aluno é diferente da disciplina selecionada");
                            }

                            var alunoDisciplinaResponse = await ConsultarAlunoDisciplinaExits(aluno.Result.AlunoId, disciplina.DisciplinaId, SemestreAtual.SemestreVigenteId);

                            if(!alunoDisciplinaResponse.Errors.Any())
                            {
                                throw new Exception($"Aluno '{aluno.Result.Nome}' não pode ser adicionado a disciplina '{disciplina.Nome}'. Aluno já cadastrado anteriormente.");
                            }

                            var disciplinaAluno = new DisciplinaAluno
                            {
                                AlunoId = aluno.Result.AlunoId,
                                DisciplinaId = request.DisciplinaId,
                                TotalAulasValidas = 0,
                                QuantidadeFalta = 0,
                                QuantidadePresenca = 0,
                                SemestreVigenteId = SemestreAtual.SemestreVigenteId
                            };

                            viewModel.EmailAlunos.Add(email);
                            _context.DisciplinasAlunos.Add(disciplinaAluno);
                            await _context.SaveChangesAsync();
                            viewModel.QtdImportados++;

                            var turnoDisciplina = disciplina.Turno == "M" ? "Manha" : "Noite";

                            var emailRequest = new EMailRequest
                            {
                                Subject = "Cadastro de disciplina - Sistema Acadêmico",
                                Body = $"Seu e-mail foi cadastrado na disciplina <b>'{disciplina.Nome}'</b> pelo professor <b>'{disciplina.Professor.Nome}'</b> <br/>" +
                                $"Turno : '{turnoDisciplina}'<br/>",
                                ToEmail = email,
                                UserName = aluno.Result.Nome
                            };

                            await _emailService.SendEmailAsync(emailRequest);
                        }
                        catch (CsvHelper.MissingFieldException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            viewModel.QtdNaoImportados++;
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

        private async Task<Response<DisciplinaAluno>> ConsultarAlunoDisciplinaExits(int alunoId, int disciplinaId, int semestreId)
        {
            return await _mediator.Send(new ConsultarAlunoDisciplinaExitsQuery
            {
                AlunoId = alunoId,
                DisciplinaId = disciplinaId,
                SemestreVigenteId = semestreId
            });
        }
    }
}