using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace SistemaAcademicoApplication.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user);
    }
}
