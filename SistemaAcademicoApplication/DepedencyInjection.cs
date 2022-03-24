using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using SistemaAcademicoApplication.Common.Behaviours;
using FluentValidation;

namespace SistemaAcademicoApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            return services;
        }
    }
}
