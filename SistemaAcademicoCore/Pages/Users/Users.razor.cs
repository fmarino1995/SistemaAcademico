using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using SistemaAcademicoApplication.Usuarios.Queries;

namespace SistemaAcademicoWeb.Pages.Users
{
    public class UsersBase : ComponentBase
    {
        [Inject]
        public IMediator _mediator { get; set; }


        protected List<ApplicationUser> Usuarios = new List<ApplicationUser>();

        protected override async Task OnInitializedAsync()
        {
            var usuariosList = await _mediator.Send(new ObterUsuariosQuery());

            Usuarios = usuariosList.Result;
        }
    }
}
