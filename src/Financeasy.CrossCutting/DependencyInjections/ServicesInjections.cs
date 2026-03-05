using Financeasy.Domain.interfaces;
using Financeasy.Domain.interfaces.Services;
using Financeasy.Domain.Services;
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
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICardPurchaseService, CardPurchaseService>();
            services.AddScoped<IInstallmentGeneratorService, InstallmentGeneratorService>();
            services.AddScoped<IInvoiceService, InvoiceService>();

            return services;
        }
    }
}