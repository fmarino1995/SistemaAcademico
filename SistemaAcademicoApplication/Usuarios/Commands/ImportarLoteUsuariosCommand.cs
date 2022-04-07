using MediatR;
using Microsoft.AspNetCore.Http;
using SistemaAcademicoApplication.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoApplication.Usuarios.Commands
{
    public class ImportarLoteUsuariosCommand : IRequest<Response<bool>>
    {
        public IFormFile File { get; set; }
    }

    public class ImportarLoteUsuariosCommandHanndler
    {



    }
}
