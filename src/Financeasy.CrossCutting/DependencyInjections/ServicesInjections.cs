using Financeasy.Domain.interfaces;
using Financeasy.Infra.Services;
using Financeasy.Infra.Util;
using Microsoft.Extensions.DependencyInjection;

namespace Financeasy.CrossCutting.DependencyInjections
{
    public static class ServicesInjections
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}