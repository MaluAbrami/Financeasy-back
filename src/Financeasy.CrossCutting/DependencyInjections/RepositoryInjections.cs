using Financeasy.Domain.interfaces;
using Financeasy.Infra.Repository;
using Financeasy.Infra.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Financeasy.CrossCutting.DependencyInjections
{
    public static class RepositoryInjections
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<object>, BaseRepository<object>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICardPurchaseRepository, CardPurchaseRepository>();
            services.AddScoped<ICardInstallmentRepository, CardInstallmentRepository>();
            services.AddScoped<ICardInvoiceRepository, CardInvoiceRepository>();
            services.AddScoped<IAlertRepository, AlertRepository>();

            return services;
        }
    }
}