using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoApplication.Roles.Commands;
using SistemaAcademicoApplication.Roles.Queries;

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

        public async Task<bool> EditRoleAsync(Role role)
        {
            try
            {
                var response = await _mediator.Send(new EditarRoleCommand { Role = role });

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

        public Task<IdentityRole> GetRoleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<IdentityRole>> GetRolesAsnyc()
        {
            try
            {
                var response = await _mediator.Send(new ObterRolesQuery());

                return response.Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> RemoveRoleAsync(string roleId)
        {
            try
            {
                var response = await _mediator.Send(new RemoverRoleCommand { RoleId = roleId });

                if (response.Result)
                    return await Task.FromResult(true);
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
