using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoApplication.Roles.Commands;

namespace SistemaAcademicoInfrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMediator _mediator;

        public RoleService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> AddRoleAsync(Role role)
        {
            try
            {
                var response = await _mediator.Send(new CriarRoleCommand { Role = role });

                if (response.Result)
                {
                    return await Task.FromResult(true);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        public Task<bool> EditRoleAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> GetRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<IdentityRole>> GetRolesAsnyc()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveRoleAsync(string roleId)
        {
            throw new NotImplementedException();
        }
    }
}
