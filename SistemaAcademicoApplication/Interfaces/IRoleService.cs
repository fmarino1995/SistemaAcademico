using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace SistemaAcademicoApplication.Interfaces
{
    public interface IRoleService
    {
        Task<IdentityRole> GetRoleAsync();
        Task<List<IdentityRole>> GetRolesAsnyc();
        Task<bool> AddRoleAsync(Role role);
        Task<bool> RemoveRoleAsync(string roleId);
        Task<bool> EditRoleAsync(Role role);
    }
}
