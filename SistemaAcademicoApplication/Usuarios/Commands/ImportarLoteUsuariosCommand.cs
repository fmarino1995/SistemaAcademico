using Domain.Entities;
using Domain.Constantes;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using CsvHelper;
using Microsoft.AspNetCore.Components.Forms;
using System.Globalization;
using CsvHelper.Configuration;
using System.Text;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoApplication.LogImportacoes.Commands;
using Domain.ViewModels;
using SistemaAcademicoApplication.UserRoles.Commands;
using Microsoft.AspNetCore.Identity;

namespace SistemaAcademicoApplication.Usuarios.Commands
{
    public class ImportarLoteUsuariosCommand : IRequest<Response<ImportarUsuariosViewModel>>
    {
        public IBrowserFile File { get; set; }
        public string FilePath { get; set; }
    }

    public class ImportarLoteUsuariosCommandHandler : IRequestHandler<ImportarLoteUsuariosCommand, Response<ImportarUsuariosViewModel>>
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        private List<ApplicationUser> usuariosImportados = new List<ApplicationUser>();


        public ImportarLoteUsuariosCommandHandler(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        public async Task<Response<ImportarUsuariosViewModel>> Handle(ImportarLoteUsuariosCommand request, CancellationToken cancellationToken)
        {
            ImportarUsuariosViewModel viewModel = new ImportarUsuariosViewModel();
            viewModel.Users = new List<ApplicationUser>();
            viewModel.LogImportacao = new LogImportacao();
            viewModel.LogImportacao.Errors = new List<string>();
            viewModel.LogImportacao.TipoImportacao = Parametros.TipoImportacaoUsuario;
            viewModel.LogImportacao.DataCriacao = DateTime.Now;
            viewModel.LogImportacao.CaminhoArquivo = request.FilePath;
            viewModel.LogImportacao.NomeArquivo = request.File.Name;

            var importacaoFolder = new DirectoryInfo("C:\\arquivosImportacaoUsuario");

            if (!importacaoFolder.Exists)
                importacaoFolder.Create();

            try
            {
                Stream stream = request.File.OpenReadStream(maxAllowedSize: 15728640); //15MB
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
                        var email = csv.GetField("EMAIL");
                        var nome = csv.GetField("NOME COMPLETO");

                        var user = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            NomeCompleto = nome,
                            Email = email,
                            Senha = GerarSenhaTemporaria(),
                            DataCriacao = DateTime.Now,
                            DataAlteracao = DateTime.Now,
                            Status = ConstantesLogin.StatusUsuarioAtivo,
                            UserName = email,
                            RoleId = ConstantesSistema.RoleAluno
                        };

                        var result = await _userService.CreateUserAsync(user);

                        if (!result.Succeeded)
                            viewModel.LogImportacao.Errors.Add($"Usuário ' {user.Email} ' não foi cadastrado devido a um erro.");
                        else
                        {
                            viewModel.Users.Add(user);
                            await _mediator.Send(new AssociarPerfilUsuarioCommand
                            {
                                UserRole = new IdentityUserRole<string>
                                {
                                    UserId = user.Id,
                                    RoleId = ConstantesSistema.RoleAluno
                                }
                            });
                        }
                            
                    }
                }

                viewModel.LogImportacao.Status = "CONCLUIDO";
                viewModel.LogImportacao.Mensagem = viewModel.LogImportacao.Errors.Count == 0 ? $"Importação concluída sem erros do arquivo {request.File.Name}"
                    : $"Importação concluída com {viewModel.LogImportacao.Errors.Count} erros no arquivo {request.File.Name}";
                await _mediator.Send(new CriarLogImportacaoCommand { LogImportacao = viewModel.LogImportacao });
                return new Response<ImportarUsuariosViewModel>(viewModel);
            }
            catch (Exception ex)
            {
                viewModel.LogImportacao.Mensagem = @"Erro ao realizar a importação" +
                    " Mensagem do sistema : " + ex.Message;
                viewModel.LogImportacao.Status = "ERRO";

                await _mediator.Send(new CriarLogImportacaoCommand { LogImportacao = viewModel.LogImportacao });

                return new Response<ImportarUsuariosViewModel>(viewModel);
            }
        }

        private string GerarSenhaTemporaria()
        {
            string chars = "abcdefghjkmnpqrstuvwxyz023456789";
            string senha = "";
            Random random = new Random();
            for (int f = 0; f < 6; f++)
            {
                senha = senha + chars.Substring(random.Next(0, chars.Length - 1), 1);
            }

            return senha;
        }
    }
}
