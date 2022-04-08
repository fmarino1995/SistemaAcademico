using Domain.Entities;
using Domain.Constantes;
using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using SistemaAcademicoData.Context;
using CsvHelper;
using Microsoft.AspNetCore.Components.Forms;
using System.Globalization;
using CsvHelper.Configuration;
using System.Text;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoApplication.LogImportacoes.Commands;

namespace SistemaAcademicoApplication.Usuarios.Commands
{
    public class ImportarLoteUsuariosCommand : IRequest<Response<LogImportacao>>
    {
        public IBrowserFile File { get; set; }
        public string FilePath { get; set; }
    }

    public class ImportarLoteUsuariosCommandHandler : IRequestHandler<ImportarLoteUsuariosCommand, Response<LogImportacao>>
    {
        private readonly SistemaAcademicoContext _context;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;
        private List<ApplicationUser> usuariosImportados = new List<ApplicationUser>();


        public ImportarLoteUsuariosCommandHandler(SistemaAcademicoContext context, IUserService userService, IEmailService emailService, IMediator mediator)
        {
            _context = context;
            _userService = userService;
            _emailService = emailService;
            _mediator = mediator;
        }

        public async Task<Response<LogImportacao>> Handle(ImportarLoteUsuariosCommand request, CancellationToken cancellationToken)
        {
            LogImportacao Log = new LogImportacao();
            Log.TipoImportacao = Parametros.TipoImportacaoUsuario;
            Log.DataCriacao = DateTime.Now;
            Log.CaminhoArquivo = request.FilePath;
            Log.NomeArquivo = request.File.Name;

            var importacaoFolder = new DirectoryInfo("C:\\arquivosImportacaoUsuario");

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
                        var email = csv.GetField("EMAIL");
                        var nome = csv.GetField("NOME");

                        var user = new ApplicationUser
                        {
                            Id = new Guid().ToString(),
                            NomeCompleto = nome,
                            Email = email,
                            Senha = GerarSenhaTemporaria(),
                            DataCriacao = DateTime.Now,
                            DataAlteracao = DateTime.Now,
                            Status = ConstantesLogin.StatusUsuarioAtivo,
                            UserName = email
                        };

                        var result = await _userService.CreateUserAsync(user);

                        if (!result.Succeeded)
                        {
                            Log.Errors.Add($"Usuário ' {user.Email} ' não foi cadastrado devido a um erro.");
                        }
                    }
                }

                Log.Status = "CONCLUIDO";
                Log.Mensagem = Log.Errors.Count == 0 ? $"Importação concluída sem erros do arquivo {request.File.Name}"
                    : $"Importação concluída com {Log.Errors.Count} erros no arquivo {request.File.Name}";
                await _mediator.Send(new CriarLogImportacaoCommand { LogImportacao = Log });
                return new Response<LogImportacao>(Log);
            }
            catch (Exception ex)
            {
                Log.Mensagem = @"Erro ao realizar a importação" +
                    " Mensagem do sistema : " + ex.Message;
                Log.Status = "ERRO";

                await _mediator.Send(new CriarLogImportacaoCommand { LogImportacao = Log });

                return new Response<LogImportacao>(Log);
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
