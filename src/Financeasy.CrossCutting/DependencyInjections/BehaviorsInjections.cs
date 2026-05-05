using Financeasy.Application.Behaviors;
using Financeasy.Application.UseCases.UserCases.RegisterUser;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Financeasy.CrossCutting.DependencyInjections
{
    public static class BehaviorsInjections
    {
        public static IServiceCollection AddBehaviors(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(typeof(RegisterUserCommand).Assembly);

            return services;
        }
    }
}