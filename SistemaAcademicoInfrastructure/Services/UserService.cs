using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using SistemaAcademicoApplication.Interfaces;

namespace SistemaAcademicoInfrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userService;

        public UserService(UserManager<ApplicationUser> userService)
        {
            _userService = userService;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user)
        {
            var result = await _userService.CreateAsync(user, user.Senha);

            if (result.Succeeded)
                return await Task.FromResult(result);
            else
                return result;
        }
    }
}
