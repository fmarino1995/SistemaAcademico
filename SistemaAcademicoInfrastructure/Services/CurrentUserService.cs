using Microsoft.AspNetCore.Http;
using SistemaAcademicoApplication.Interfaces;

namespace SistemaAcademicoInfrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Task<string> GetUserName()
        {
            return Task.FromResult(_httpContext.HttpContext.User.Identity.Name);
        }
    }
}