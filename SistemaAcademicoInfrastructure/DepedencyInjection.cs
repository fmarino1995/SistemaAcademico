using Microsoft.Extensions.DependencyInjection;
using SistemaAcademicoApplication.Interfaces;
using SistemaAcademicoInfrastructure.Services;

namespace SistemaAcademicoInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IToastService, ToastService>();

            return services;
        }
    }
}
